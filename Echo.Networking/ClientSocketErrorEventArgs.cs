using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

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