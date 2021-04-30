using System;

namespace Echo.Networking
{
    public sealed class ClientSocketStateChangedEventArgs : EventArgs
    {
        public ClientSocketState State { get; }

        public ClientSocketStateChangedEventArgs(ClientSocketState state)
        {
            State = state;
        }
    }
}