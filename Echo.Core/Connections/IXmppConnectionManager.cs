namespace Echo.Core.Connections
{
    public interface IXmppConnectionManager
    {
        int Count { get; }
        IXmppConnection? ActiveConnection { get; }

        IXmppConnection? GetConnection(int connectionIndex);
        IXmppConnection? GetConnection(XmppUri accountAddress);
    }
}