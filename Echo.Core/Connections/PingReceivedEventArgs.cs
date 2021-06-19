using System;

namespace Echo.Core.Connections
{
    public sealed class PingReceivedEventArgs : EventArgs
    {
        public XmppUri Sender { get; }
        public IXmppConnection Connection { get; }

        public PingReceivedEventArgs(XmppUri sender, IXmppConnection connection)
        {
            Sender = sender;
            Connection = connection;
        }
    }
}