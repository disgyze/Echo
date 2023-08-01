namespace Echo.Core.Connections
{
    public readonly struct ActiveConnectionChangedEventArgs
    {
        public XmppConnectionService? OldConnection { get; }
        public XmppConnectionService? NewConnection { get; }

        public ActiveConnectionChangedEventArgs(XmppConnectionService? oldConnection, XmppConnectionService? newConnection)
        {
            OldConnection = oldConnection;
            NewConnection = newConnection;
        }
    }
}