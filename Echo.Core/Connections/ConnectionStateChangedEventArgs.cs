using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionStateChangedEventArgs : EventArgs
    {
        public IXmppConnection Client { get; }
        public ConnectionState State { get; }

        public ConnectionStateChangedEventArgs(IXmppConnection client, ConnectionState state)
        {
            Client = client;
            State = state;
        }
    }
}