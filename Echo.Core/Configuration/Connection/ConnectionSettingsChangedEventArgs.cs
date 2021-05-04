namespace Echo.Core.Configuration.Connection
{
    public sealed class ConnectionSettingsChangedEventArgs : SettingsChangedEventArgs<ConnectionSettings>
    {
        public ConnectionSettingsChangedEventArgs(ConnectionSettings? oldSettings, ConnectionSettings? newSettings) : base(oldSettings, newSettings)
        {
        }
    }
}