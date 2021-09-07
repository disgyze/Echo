namespace Echo.Core.Configuration.User
{
    public interface IUserConfigurationStore
    {
        AccountSettings? Get(XmppAddress accountAddress);
        AccountSettings? GetOrCreate(XmppAddress accountAddress);
    }
}