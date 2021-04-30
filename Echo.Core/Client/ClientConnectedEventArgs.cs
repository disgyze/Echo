using System;

namespace Echo.Core.Client
{
    public sealed class ClientConnectedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public ClientConnectedEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}