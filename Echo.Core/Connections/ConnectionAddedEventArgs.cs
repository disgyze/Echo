using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionAddedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }

        public ConnectionAddedEventArgs(IXmppConnection connection)
        {
            Connection = connection;
        }
    }
}