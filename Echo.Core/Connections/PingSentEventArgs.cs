namespace Echo.Core.Connections
{
    public readonly struct PingSentEventArgs
    {
        public XmppConnectionService Connection { get; }

        public PingSentEventArgs(XmppConnectionService connection)
        {
            Connection = connection;
        }
    }
}