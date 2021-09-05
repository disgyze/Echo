using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionRemovedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }

        public ConnectionRemovedEventArgs(IXmppConnection connection)
        {
            Connection = connection;
        }
    }
}