using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ACNHPoker
{
    public class SysBot : ISysBot
    {
        Socket _socket;

        public SysBot(Socket socket)
        {
            _socket = socket;
        }

        public bool SupportsFreeze
        {
            get { return true; }
        }

        public bool Connected
        {
            get { return _socket != null && _socket.Connected; }
        }

        public string Id
        {
            get { return "Sys"; }
        }

        public void Close()
        {
            _socket.Close();
            _socket = null;
        }

        public bool IsConnected()
        {
            if (!Connected)
                return false;

            try
            {
                return !(_socket.Poll(1, SelectMode.SelectRead) && _socket.Available == 0);
            }
            catch (SocketException) { return false; }
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

            int startTickCount = Environment.TickCount;
            int sent = 0;  // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    sent += _socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        //Thread.Sleep(10);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (sent < size);
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
                try
                {
                    received += _socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        //Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (received < size && buffer[received - 1] != 0xA);
            return received;
        }

        string ReadToIntermediateString(string command, long address, int size)
        {
            string msg = String.Format("{2} 0x{0:X8} {1}\r\n", address, size, command);
            SendString(Encoding.UTF8.GetBytes(msg));
            byte[] b = new byte[size * 2 + 64];
            int first_rec = ReceiveString(b);
            return Encoding.ASCII.GetString(b, 0, size * 2);
        }

        bool _SendByteArray(string command, int maxBytesTosend, long initAddr, byte[] buffer, int size, ref int counter)
        {
            int sent = 0;
            int bytesToSend = 0;
            StringBuilder dataTemp = new StringBuilder();
            string msg;
            while (sent < size)
            {
                dataTemp.Clear();
                bytesToSend = (size - sent > maxBytesTosend) ? maxBytesTosend : size - sent;
                for (int i = 0; i < bytesToSend; i++)
                {
                    dataTemp.Append(String.Format("{0:X2}", buffer[sent + i]));
                }
                msg = String.Format("{2} 0x{0:X8} 0x{1}\r\n", initAddr + sent, dataTemp.ToString(), command);
                SendString(Encoding.UTF8.GetBytes(msg));
                sent += bytesToSend;
                counter++;
            }

            return false;
        }

        void _ReadUInt32Array(long initAddr, UInt32[] buffer, int size, int offset = 0)
        {
            // Read in small chunks
            const int maxBytesToReceive = 1536;  // Absolutely needs to be multiple of 4
            int received = 0;
            int bytesToReceive;
            while (received < size)
            {
                bytesToReceive = (size - received > maxBytesToReceive) ? maxBytesToReceive : size - received;
                string bufferRepr = ReadToIntermediateString("peek", initAddr + received, bytesToReceive);
                for (int i = 0; i < (bytesToReceive / 4); i++)
                {
                    buffer[offset + (received / 4) + i] = Convert.ToUInt32(bufferRepr.Substring(i * 8 + 6, 2) +
                                                bufferRepr.Substring(i * 8 + 4, 2) +
                                                bufferRepr.Substring(i * 8 + 2, 2) +
                                                bufferRepr.Substring(i * 8, 2), 16);
                }
                received += bytesToReceive;
            }
        }

        bool _SendUInt32Array(long initAddr, UInt32[] buffer, int size, int offset = 0)
        {
            // Send in small chunks
            const int maxUInt32Tosend = 125;
            size /= 4;
            int sent = 0;
            int UInt32ToSend = 0;
            StringBuilder dataTemp = new StringBuilder();
            string msg;
            while (sent < size)
            {
                dataTemp.Clear();
                UInt32ToSend = (size - sent > maxUInt32Tosend) ? maxUInt32Tosend : size - sent;
                for (int i = 0; i < UInt32ToSend; i++)
                {
                    dataTemp.Append(String.Format("{0:X2}{1:X2}{2:X2}{3:X2}",
                        (buffer[offset + sent + i] & 0xFF), (buffer[offset + sent + i] & 0xFF00) >> 8,
                        (buffer[offset + sent + i] & 0xFF0000) >> 16, (buffer[offset + sent + i] & 0xFF000000) >> 24));
                }
                msg = String.Format("poke 0x{0:X8} 0x{1}\r\n", initAddr + sent * 4, dataTemp.ToString());
                Debug.Print(msg);
                SendString(Encoding.UTF8.GetBytes(msg));
                sent += UInt32ToSend;
            }

            return false;
        }

        byte[] DoReadByteArray(string command, int maxBytesToReceive, long address, int length, ref int counter)
        {
            byte[] result = new byte[length];
            
            int received = 0;
            int bytesToReceive;
            while (received < length)
            {
                bytesToReceive = (length - received > maxBytesToReceive) ? maxBytesToReceive : length - received;
                string bufferRepr = ReadToIntermediateString(command, address + received, bytesToReceive);
                if (bufferRepr == null)
                {
                    return null;
                }
                for (int i = 0; i < bytesToReceive; i++)
                {
                    result[received + i] = Convert.ToByte(bufferRepr.Substring(i * 2, 2), 16);
                }
                received += bytesToReceive;
                ++counter;
            }
            return result;
        }

        public byte[] ReadByteArray(long address, int length, ref int counter)
        {
            const int maxBytesToReceive = 1536;
            return DoReadByteArray("peek", maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray(long address, int length)
        {
            const int maxBytesToReceive = 1536;
            int counter = 0;
            return DoReadByteArray("peek", maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray8KWithCounter(long address, int length, ref int counter)
        {
            const int maxBytesToReceive = 8192;
            return DoReadByteArray("peek", maxBytesToReceive, address, length, ref counter);
        }

        public byte[] ReadByteArray8K(long address, int length)
        {
            const int maxBytesToReceive = 8192;
            int counter = 0;
            return DoReadByteArray("peek", maxBytesToReceive, address, length, ref counter);
        }

        public void WriteByteArray(string value, long address, int writeSize)
        {
            System.Diagnostics.Debug.Assert(address <= 0xFFFFFFFF);
            string msg = String.Format("poke 0x{0:X8} 0x{1}\r\n", address.ToString("X"), Utilities.flip(Utilities.precedingZeros(value, writeSize)));
            SendString(Encoding.UTF8.GetBytes(msg));
        }

        public void SendByteArrayWithCounter(byte[] buffer, long address, ref int counter)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;

            const int maxBytesTosend = 1536;
            _SendByteArray("poke", maxBytesTosend, address, buffer, bufferLen, ref counter);
        }

        public void SendByteArray(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;

            const int maxBytesTosend = 1536;
            int counter = 0;
            _SendByteArray("poke", maxBytesTosend, address, buffer, bufferLen, ref counter);
        }

        public void SendByteArray8KWithCounter(byte[] buffer, long address, ref int counter)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;

            const int maxBytesTosend = 8192;
            _SendByteArray("poke", maxBytesTosend, address, buffer, bufferLen, ref counter);
        }

        public void SendByteArray8K(byte[] buffer, long address)
        {
            int counter = 0;
            SendByteArray8KWithCounter(buffer, address, ref counter);
        }

        public byte[] ReadByteArrayMain(long address, int length)
        {
            const int maxBytesTosend = 1536;
            int counter = 0;
            return DoReadByteArray("peekMain", maxBytesTosend, address, length, ref counter);
        }

        public void SendByteArrayMain(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;

            const int maxBytesToReceive = 1536;
            int counter = 0;
            _SendByteArray("pokeMain", maxBytesToReceive, address, buffer, bufferLen, ref counter);
        }

        public byte[] ReadByteArrayAbsolute(long address, int length)
        {
            const int maxBytesToReceive = 1536;
            int counter = 0;
            return DoReadByteArray("peekAbsolute", maxBytesToReceive, address, length, ref counter);
        }

        public void SendByteArrayAbsolute(byte[] buffer, long address)
        {
            int bufferLen = buffer.Length;
            if (bufferLen == 0)
                return;

            const int maxBytesTosend = 1536;
            int counter = 0;
            _SendByteArray("pokeAbsolute", maxBytesTosend, address, buffer, bufferLen, ref counter);
        }

        public void ReadUInt32Array(long address, UInt32[] buffer, int offset, int count)
        {
            _ReadUInt32Array(address, buffer, count * 4, offset);
        }

        public void WriteUInt32Array(long address, UInt32[] buffer, int offset, int count)
        {
            _SendUInt32Array(address, buffer, count * 4, offset);
        }
    }
}
