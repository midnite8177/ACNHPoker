using System;

namespace ACNHPoker
{
    public interface ISysBot
    {
        string Id { get; }
        bool Connected { get; }

        bool SupportsFreeze { get; }

        void Close();
        bool IsConnected();

        void SendString(byte[] buffer, int offset = 0, int size = 0, int timeout = 100);
        int ReceiveString(byte[] buffer, int offset = 0, int size = 0, int timeout = 30000);

        byte[] ReadByteArray(long address, int length);
        byte[] ReadByteArray(long address, int length, ref int counter);

        void WriteByteArray(string value, long address, int writeSize);

        void SendByteArrayWithCounter(byte[] buffer, long address, ref int counter);
        void SendByteArray(byte[] buffer, long address);

        void SendByteArray8KWithCounter(byte[] buffer, long address, ref int counter);
        void SendByteArray8K(byte[] buffer, long address);

        byte[] ReadByteArray8K(long address, int length);
        byte[] ReadByteArray8KWithCounter(long address, int length, ref int counter);


        byte[] ReadByteArrayMain(long address, int length);
        void SendByteArrayMain(byte[] buffer, long address);

        byte[] ReadByteArrayAbsolute(long address, int length);
        void SendByteArrayAbsolute(byte[] buffer, long address);

        void ReadUInt32Array(long address, UInt32[] buffer, int offset, int count);
        void WriteUInt32Array(long address, UInt32[] buffer, int offset, int count);
    }
}
