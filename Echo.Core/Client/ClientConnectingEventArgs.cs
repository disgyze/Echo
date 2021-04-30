using System;

namespace Echo.Core.Client
{
    public sealed class ClientConnectingEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public ClientConnectingEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}