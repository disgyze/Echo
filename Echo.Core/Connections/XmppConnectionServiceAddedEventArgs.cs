namespace Echo.Core.Connections
{
    public readonly struct XmppConnectionServiceAddedEventArgs
    {
        public XmppConnectionService Connection { get; }

        public XmppConnectionServiceAddedEventArgs(XmppConnectionService connection)
        {
            Connection = connection;
        }
    }
}