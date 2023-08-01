namespace Echo.Core.Connections
{
    public readonly struct PingReceivedEventArgs
    {
        public XmppAddress Sender { get; }
        public XmppConnectionService Connection { get; }

        public PingReceivedEventArgs(XmppAddress sender, XmppConnectionService connection)
        {
            Sender = sender;
            Connection = connection;
        }
    }
}