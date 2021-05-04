namespace Echo.Core.Configuration.User
{
    public sealed class AccountSettingsChangedEventArgs : SettingsChangedEventArgs<AccountSettings>
    {
        public AccountSettingsChangedEventArgs(AccountSettings? oldSettings, AccountSettings? newSettings) : base(oldSettings, newSettings)
        {
        }
    }
}