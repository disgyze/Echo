using System;

namespace Echo.Core.Client
{
    public sealed class PingReceivedEventArgs : EventArgs
    {
        public XmppUri Sender { get; }
        public IXmppClient Connection { get; }

        public PingReceivedEventArgs(XmppUri sender, IXmppClient connection)
        {
            Sender = sender;
            Connection = connection;
        }
    }
}