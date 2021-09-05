namespace Echo.Core.Configuration.User
{
    public interface IUserConfigurationStore
    {
        AccountSettings? Get(XmppUri accountAddress);
        AccountSettings? GetOrCreate(XmppUri accountAddress);
    }
}