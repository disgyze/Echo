using System;

namespace Echo.Core.Connections
{
    public sealed class PingSentEventArgs : EventArgs
    {
        public IXmppClient Client { get; }

        public PingSentEventArgs(IXmppClient client)
        {
            Client = client;
        }
    }
}