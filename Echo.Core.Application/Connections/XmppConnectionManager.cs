namespace Echo.Core.Connections
{
    public abstract class XmppConnectionManager : IXmppConnectionManager
    {
        public abstract int Count { get; }
        public abstract XmppConnection? ActiveConnection { get; }

        public abstract XmppConnection? GetConnection(int connectionIndex);
        public abstract XmppConnection? GetConnection(XmppAddress accountAddress);

        IXmppConnection? IXmppConnectionManager.ActiveConnection => ActiveConnection;
        IXmppConnection? IXmppConnectionManager.GetConnection(int connectionIndex) => GetConnection(connectionIndex);
        IXmppConnection? IXmppConnectionManager.GetConnection(XmppAddress accountAddress) => GetConnection(accountAddress);
    }
}