using System;
using Echo.Xmpp;

namespace Echo.Core.Client
{
    public sealed class PingReceivedEventArgs : EventArgs
    {
        public XmppAddress Sender { get; }
        public IXmppClient Connection { get; }

        public PingReceivedEventArgs(XmppAddress sender, IXmppClient connection)
        {
            Sender = sender;
            Connection = connection;
        }
    }
}