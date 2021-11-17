using System;

namespace ACNHPoker
{
    public class UsbBot : ISysBot
    {
        USBBot _usbBot;

        public UsbBot(USBBot usbBot)
        {
            _usbBot = usbBot;
        }

        public bool Connected
        {
            get { return _usbBot != null && _usbBot.Connected; }
        }

        public string Id
        {
            get { return "Usb"; }
        }

        public bool SupportsFreeze
        {
            get { return false; }
        }

        public bool IsConnected()
        {
            return Connected;
        }

        public void Close()
        {
            _usbBot.Disconnect();
            _usbBot = null;
        }

        public void SendString(byte[] buffer, int offset = 0, int size = 0, int timeout = 100)
        {
            if (size == 0)
            {
                for (int i = offset; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0xA)
                    {
                        size = i + 1 - offset;
                        break;
                    }
                }

                if (size == 0)
                {
                    size = buffer.Length - offset;
                }
            }

            _usbBot.WriteRawBytes(buffer, offset, size);
        }

        public int ReceiveString(byte[] buffer, int offset = 0, int size = 0, int timeout = 30000)
        {
            if (size == 0)
            {
                size = buffer.Length - offset;
            }

            int startTickCount = Environment.TickCount;
            int received = 0;  // how many bytes is already received
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");

                if( offset == 0 && received == 0 && size == buffer.Length )
                {
                    received += _usbBot.Read(buffer);
                }
                else
                {
                    byte[] temp = new byte[size - received];
                    int count = _usbBot.Read(temp);
                    temp.CopyTo(buffer, offset + received);
                    received += count;
                }

            } while (received < size && buffer[received - 1] != 0xA);
            return received;
        }

        public byte[] ReadByteArrayMain(long address, int length)
        {
            // Read in small chunks
            byte[] result = new byte[length];
            const int maxBytesToReceive = 468;
            int received = 0;
            int bytesToReceive;
            while (received < length)
            {
                bytesToReceive = (length - received > maxBytesToReceive) ? maxBytesToReceive : length - received;
                byte[] buffer = _usbBot.ReadBytesMain(address + received, bytesToReceive);
                for (int i = 0; i < bytesToReceive; i++)
                {
                    result[received + i] = buffer[i];
                }
                received += bytesToReceive;
            }
            return result;
        }

        public byte[] ReadByteArrayAbsolute(long address, int length)
        {
            // Read in small chunks
            byte[] result = new byte[length];
            const int maxBytesToReceive = 468;
            int received = 0;
            int bytesToReceive;
            while (received < length)
            {
                bytesToReceive = (length - received > maxBytesToReceive) ? maxBytesToReceive : length - received;
                byte[] buffer = _usbBot.ReadBytesAbsolute(address + received, bytesToReceive);
                for (int i = 0; i < bytesToReceive; i++)
                {
                    result[received + i] = buffer[i];
                }
                received += bytesToReceive;
            }
            return result;
        }

        byte[] _DoReadByteArray(int maxBytesToReceive, long address, int length, ref int counter)
        {
            // Read in small chunks
            byte[] result = new byte[length];
            int received = 0;
            int bytesToReceive;
            while (received < length)
            {
                bytesToReceive = (length - received > maxBytesToReceive) ? maxBytesToReceive : length - received;
                byte[] buffer = _usbBot.ReadBytes(address + received, bytesToReceive);
                for (int i = 0; i < bytesToReceive; i++)
                {
                    result[received + i] = buffer[i];
                }
                received += bytesToReceive;
                counter++;
            }
            return result;
        }

        public byte[] ReadByteArray(long address, int length, ref int counter)
        {
            int maxBytesToReceive = _usbBot.MaximumTransferSize;
            return _DoReadByteArray(maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray(long address, int length)
        {
            int maxBytesToReceive = _usbBot.MaximumTransferSize;
            int counter = 0;
            return _DoReadByteArray(maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray8KWithCounter(long address, int length, ref int counter)
        {
            int maxBytesToReceive = _usbBot.MaximumTransferSize;
            return _DoReadByteArray(maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray8K(long address, int length)
        {
            int maxBytesToReceive = _usbBot.MaximumTransferSize;
            int counter = 0;
            return _DoReadByteArray(maxBytesToReceive, address, length, ref counter);
        }

        public void WriteByteArray(string value, long address, int writeSize)
        {
            _usbBot.WriteBytes(Utilities.stringToByte(Utilities.flip(Utilities.precedingZeros(value, writeSize))), address);
        }

        public void SendByteArrayWithCounter(byte[] buffer, long address, ref int counter)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytes(buffer, address);
            ++counter;
        }

        public void SendByteArray(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytes(buffer, address);
        }

        public void SendByteArray8KWithCounter(byte[] buffer, long address, ref int counter)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytes(buffer, address);
            ++counter;
        }

        public void SendByteArray8K(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytes(buffer, address);
        }

        public void SendByteArrayMain(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytesMain(buffer, address);
        }

        public void SendByteArrayAbsolute(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;
            _usbBot.WriteBytesAbsolute(buffer, address);
        }

        public void ReadUInt32Array(long address, UInt32[] buffer, int offset, int count)
        {
            byte[] b = _usbBot.ReadBytes(address, count * 4);
            for (int i = 0; i < count; ++i)
            {
                buffer[offset + i] = BitConverter.ToUInt32(b, i * 4);
            }
        }

        public void WriteUInt32Array(long address, UInt32[] buffer, int offset, int count)
        {
            byte[] b = new byte[count * 4];
            for( int i = 0; i < count; ++i )
            {
                BitConverter.GetBytes(buffer[offset + i]).CopyTo(b, i * 4);
            }
            _usbBot.WriteBytes(b, address);
        }
    }
}
