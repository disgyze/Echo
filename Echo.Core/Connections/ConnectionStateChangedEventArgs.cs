using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionStateChangedEventArgs : EventArgs
    {
        public IXmppClient Client { get; }
        public ConnectionState State { get; }

        public ConnectionStateChangedEventArgs(IXmppClient client, ConnectionState state)
        {
            Client = client;
            State = state;
        }
    }
}