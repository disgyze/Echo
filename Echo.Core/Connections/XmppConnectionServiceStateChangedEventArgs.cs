namespace Echo.Core.Connections
{
    public readonly struct XmppConnectionServiceStateChangedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public XmppConnectionServiceState State { get; }

        public XmppConnectionServiceStateChangedEventArgs(XmppConnectionService connection, XmppConnectionServiceState state)
        {
            Connection = connection;
            State = state;
        }
    }
}