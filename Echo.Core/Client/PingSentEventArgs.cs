using System;

namespace Echo.Core.Client
{
    public sealed class PingSentEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public PingSentEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}