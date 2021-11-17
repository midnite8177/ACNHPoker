using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ACNHPoker
{

    public class USBBot
    {

        private UsbDevice SwDevice;
        private UsbEndpointReader reader;
        private UsbEndpointWriter writer;

        public int MaximumTransferSize { get { return 468; } }

        private static readonly Encoding Encoder = Encoding.UTF8;
        private static byte[] Encode(string command, bool addrn = true) => Encoder.GetBytes(addrn ? command + "\r\n" : command);

        public static byte[] PokeRaw(long offset, byte[] data) => Encode($"poke 0x{offset:X8} 0x{string.Concat(data.Select(z => $"{z:X2}"))}", false);

        public static byte[] PeekRaw(long offset, int count) => Encode($"peek 0x{offset:X8} {count}", false);

        public static byte[] PokeMain(long offset, byte[] data) => Encode($"pokeMain 0x{offset:X8} 0x{string.Concat(data.Select(z => $"{z:X2}"))}", false);

        public static byte[] PeekMain(long offset, int count) => Encode($"peekMain 0x{offset:X8} {count}", false);

        public static byte[] PokeAbsolute(long offset, byte[] data) => Encode($"pokeAbsolute 0x{offset:X8} 0x{string.Concat(data.Select(z => $"{z:X2}"))}", false);

        public static byte[] PeekAbsolute(long offset, int count) => Encode($"peekAbsolute 0x{offset:X8} {count}", false);

        public bool Connected { get; private set; }

        private readonly object _sync = new object();

        public bool Connect()
        {
            lock (_sync)
            {
                // Find and open the usb device.
                //SwDevice = UsbDevice.OpenUsbDevice(SwFinder);
                //UsbDeviceFinder finder = new UsbDeviceFinder(1406, 12288);
                //this.SwDevice = UsbDevice.OpenUsbDevice(finder);

                foreach (UsbRegistry ur in UsbDevice.AllDevices)
                {
                    if (ur.Vid == 0x57e && ur.Pid == 0x3000 && ur.Device != null)
                    {
                        SwDevice = ur.Device;
                    }
                    //System.Diagnostics.Debug.Print(ur.Vid.ToString() + " " + ur.Pid.ToString() + " " + ur.FullName.ToString() + " " + ur.IsAlive.ToString() + "\n");
                }
                Debug.Print("UsbDevice.AllDevices -> Exception thrown: 'System.ArgumentException' in mscorlib.dll");

                //SwDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready

                if (SwDevice == null)
                {
                    myMessageBox.Show("Please try to install the standalone executable of LibUsbDotNet v2.2.8.\n" +
                        "https://sourceforge.net/projects/libusbdotnet/files/LibUsbDotNet/LibUsbDotNet%20v2.2.8/ \n\n" +
                        "And then create a device filter for your Nintendo Switch.", "Device Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (SwDevice.IsOpen)
                    SwDevice.Close();
                SwDevice.Open();

                if (SwDevice is IUsbDevice wholeUsbDevice)
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.

                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface #0.
                    bool resagain = wholeUsbDevice.ClaimInterface(0);
                    if (!resagain)
                    {
                        wholeUsbDevice.ReleaseInterface(0);
                        wholeUsbDevice.ClaimInterface(0);
                    }
                }
                else
                {
                    Disconnect();
                    throw new Exception("Device is using WinUSB driver. Use libusbK and create a filter");
                }

                // open read write endpoints 1.
                reader = SwDevice.OpenEndpointReader(ReadEndpointID.Ep01);
                writer = SwDevice.OpenEndpointWriter(WriteEndpointID.Ep01);

                Connected = true;
                return true;
            }
        }

        public void Disconnect()
        {
            lock (_sync)
            {
                if (SwDevice != null)
                {
                    if (SwDevice.IsOpen)
                    {
                        if (SwDevice is IUsbDevice wholeUsbDevice)
                            wholeUsbDevice?.ReleaseInterface(0);
                        SwDevice.Close();
                    }
                }

                reader?.Dispose();
                writer?.Dispose();
                Connected = false;
            }
        }

        void ReadInternalHelper(byte[] buffer, out int bufferTransferred)
        {
            int retryCount = 0;

            while (true)
            {
                var ec = reader.Read(buffer, 2000, out bufferTransferred);
                if (ec == ErrorCode.IoTimedOut)
                {
                    System.Diagnostics.Debug.Assert(bufferTransferred == 0);
                    Thread.Sleep(1);
                    ++retryCount;
                    if (retryCount == 30)
                        throw new Exception("USBBot - Timeout");
                    continue;
                }

                if (ec != ErrorCode.None)
                {
                    Disconnect();
                    throw new Exception(UsbDevice.LastErrorString);
                }

                if (bufferTransferred != buffer.Length)
                {
                    Console.WriteLine($"READ: {bufferTransferred}, SHOULD BE {buffer.Length}");
                }

                return;
            }
        }

        void SendInternalHelper(byte[] buffer, out int bufferTransferred)
        {
            int retryCount = 0;

            while (true)
            {
                var ec = writer.Write(buffer, 2000, out bufferTransferred);
                if (ec == ErrorCode.IoTimedOut)
                {
                    System.Diagnostics.Debug.Assert(bufferTransferred == 0);
                    Thread.Sleep(1);
                    ++retryCount;
                    if (retryCount == 30)
                        throw new Exception("USBBot - Timeout");
                    continue;
                }

                if (ec != ErrorCode.None)
                {
                    Disconnect();
                    throw new Exception(UsbDevice.LastErrorString);
                }

                if (bufferTransferred != buffer.Length)
                {
                    Console.WriteLine($"WROTE: {bufferTransferred}, SHOULD BE {buffer.Length}");
                }

                return;
            }
        }

        private int ReadInternal(byte[] buffer)
        {
            if (reader == null)
                throw new Exception("USB writer is null, you may have disconnected the device during previous function");

            // read the size of the transfer ahead (4-bytes)
            byte[] sizeOfReturn = new byte[4];
            ReadInternalHelper(sizeOfReturn, out var xferLengthLen);
            if (xferLengthLen != 4)
                throw new Exception("USB Read: Expected 4 bytes");

            // how much data is coming?
            uint xferLength = BitConverter.ToUInt32(sizeOfReturn, 0);

            if (xferLength == (buffer.Length * 2) + 1)
            {
                // sysbot-base mode of ASCII transmission
                byte[] tempBuffer = new byte[xferLength];
                ReadInternalHelper(tempBuffer, out var lenTempBuffer);
                if (lenTempBuffer != xferLength)
                {
                    Console.WriteLine("USB READ: Read {0} bytes, but there are {1} in the pipe", lenTempBuffer, xferLength);
                }

                // ugh.
                string str = ASCIIEncoding.ASCII.GetString(tempBuffer);

                int bufferLen = buffer.Length;
                for (int i = 0; i < bufferLen; i++)
                {
                    buffer[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                }

                return bufferLen;
            }

            if (xferLength != buffer.Length)
            {
                Console.WriteLine("USB READ: {0} bytes in, but only reading {1}", xferLength, buffer.Length);
            }

            // read the payload (xferLength bytes)
            ReadInternalHelper(buffer, out var lenVal);
            if (lenVal != xferLength)
            {
                Console.WriteLine("USB READ: Read {0} bytes, but there are {1} in the pipe", lenVal, xferLength);
            }

            return lenVal;
        }

        private int SendInternal(byte[] buffer)
        {
            if (writer == null)
                throw new Exception("USB writer is null, you may have disconnected the device during previous function");

            // Write the size of the payload (4-bytes)
            uint pack = (uint)buffer.Length;
            SendInternalHelper(BitConverter.GetBytes(pack), out var sizeWritten);
            if (sizeWritten != 4)
            {
                Console.WriteLine($"USB WRITE: Expected to transfer 4 bytes, but only sent {sizeWritten}");
            }

            // Send the payload
            SendInternalHelper(buffer, out var payloadXfered);
            if (payloadXfered != pack)
            {
                Console.WriteLine($"USB WRITE: Expected to transfer {pack} bytes, but only sent {payloadXfered}");
            }

            return payloadXfered;
        }

        public int Read(byte[] buffer)
        {
            lock (_sync)
            {
                return ReadInternal(buffer);
            }
        }

        public byte[] ReadBytes(long offset, int length)
        {
            lock (_sync)
            {
                var cmd = PeekRaw(offset, length);
                SendInternal(cmd);

                // give it time to push data back
                ReadWait(length);

                var buffer = new byte[length];
                var _ = ReadInternal(buffer);
                return buffer;
            }
        }

        public byte[] ReadBytesMain(long offset, int length)
        {
            lock (_sync)
            {
                var cmd = PeekMain(offset, length);
                SendInternal(cmd);

                // give it time to push data back
                ReadWait(length);

                var buffer = new byte[length];
                var _ = ReadInternal(buffer);
                return buffer;
            }
        }

        public byte[] ReadBytesAbsolute(long offset, int length)
        {
            lock (_sync)
            {
                var cmd = PeekAbsolute(offset, length);
                SendInternal(cmd);

                // give it time to push data back
                ReadWait(length);

                var buffer = new byte[length];
                var _ = ReadInternal(buffer);
                return buffer;
            }
        }

        private void ReadWait(int numBytesRead)
        {
            //Thread.Sleep((numBytesRead / 256) + 100);
            //Thread.Sleep(1);
        }

        private void WriteWait(int numBytesWritten)
        {
            //int numMilli = (numBytesWritten / 256) + 100;
            //Thread.Sleep(numMilli);
            //Thread.Sleep(1);
        }

        public void WriteRawBytes(byte[] data, int offset, int len)
        {
            lock (_sync)
            {
                if (offset == 0 && len == data.Length)
                {
                    SendInternal(data);
                }
                else
                {
                    byte[] subData = SubArray(data, offset, len);
                    SendInternal(subData);
                }

                // give it time to push data back
                WriteWait(len);
            }
        }

        public void WriteBytes(byte[] data, long offset)
        {
            lock (_sync)
            {
                SendInternal(PokeRaw(offset, data));

                // give it time to push data back
                WriteWait(data.Length);
            }
        }

        public void WriteBytesMain(byte[] data, long offset)
        {
            lock (_sync)
            {
                SendInternal(PokeMain(offset, data));

                // give it time to push data back
                WriteWait(data.Length);
            }
        }

        public void WriteBytesAbsolute(byte[] data, long offset)
        {
            lock (_sync)
            {
                SendInternal(PokeAbsolute(offset, data));

                // give it time to push data back
                WriteWait(data.Length);
            }
        }

        private static T[] SubArray<T>(T[] data, int index, int length)
        {
            if (index + length > data.Length)
                length = data.Length - index;
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
