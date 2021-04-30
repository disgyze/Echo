using System;

namespace Echo.Networking
{
    public sealed class DataSentEventArgs : EventArgs
    {
        public byte[] Data { get; }

        public DataSentEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}