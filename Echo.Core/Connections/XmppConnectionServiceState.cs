namespace Echo.Core.Connections
{
    public enum XmppConnectionServiceState
    {
        Opening,
        Opened,
        Closed,
        WaitingForReconnection
    }
}