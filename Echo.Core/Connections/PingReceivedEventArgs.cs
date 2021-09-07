using System;

namespace Echo.Core.Connections
{
    public sealed class PingReceivedEventArgs : EventArgs
    {
        public XmppAddress Sender { get; }
        public IXmppConnection Connection { get; }

        public PingReceivedEventArgs(XmppAddress sender, IXmppConnection connection)
        {
            Sender = sender;
            Connection = connection;
        }
    }
}