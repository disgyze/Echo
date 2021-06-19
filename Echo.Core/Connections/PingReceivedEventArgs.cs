using System;

namespace Echo.Core.Connections
{
    public sealed class PingReceivedEventArgs : EventArgs
    {
        public XmppUri Sender { get; }
        public IXmppClient Client { get; }

        public PingReceivedEventArgs(XmppUri sender, IXmppClient client)
        {
            Sender = sender;
            Client = client;
        }
    }
}