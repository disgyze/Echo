namespace Echo.Core.Configuration.Connection
{
    public abstract class ConnectionSettingsManager
    {
        public abstract ConnectionSecuritySettings GetForAccount(XmppAddress accountAddress);
    }
}