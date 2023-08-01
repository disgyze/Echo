namespace Echo.Core.Connections
{
    public readonly struct XmppConnectionServiceConnectionRemovedEventArgs
    {
        public XmppConnectionService Connection { get; }

        public XmppConnectionServiceConnectionRemovedEventArgs(XmppConnectionService connection)
        {
            Connection = connection;
        }
    }
}