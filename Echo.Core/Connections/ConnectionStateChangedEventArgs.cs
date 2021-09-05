using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionStateChangedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public ConnectionState State { get; }

        public ConnectionStateChangedEventArgs(IXmppConnection connection, ConnectionState state)
        {
            Connection = connection;
            State = state;
        }
    }
}