using System;

namespace Echo.Core.Client
{
    public sealed class ClientDisconnectedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public ClientDisconnectedEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}