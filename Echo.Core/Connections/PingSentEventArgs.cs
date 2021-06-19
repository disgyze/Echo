using System;

namespace Echo.Core.Connections
{
    public sealed class PingSentEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }

        public PingSentEventArgs(IXmppConnection client)
        {
            Connection = client;
        }
    }
}