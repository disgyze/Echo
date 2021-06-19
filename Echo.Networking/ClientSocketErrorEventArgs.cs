using System;
using System.Net.Sockets;

namespace Echo.Networking
{
    public sealed class ClientSocketErrorEventArgs : EventArgs
    {
        public SocketException Error { get; }

        public ClientSocketErrorEventArgs(SocketException error)
        {
            Error = error;
        }
    }
}