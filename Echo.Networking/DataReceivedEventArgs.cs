using System;

namespace Echo.Networking
{
    public sealed class DataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; }

        public DataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}