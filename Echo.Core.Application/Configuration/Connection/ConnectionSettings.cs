namespace Echo.Core.Configuration.Connection
{
    public sealed class ConnectionSettings
    {
        static readonly ConnectionSettings defaultSettings = new ConnectionSettings();

        public static ConnectionSettings Default => defaultSettings;

        public string? Host { get; }
        public int Port { get; }
        public int ReconnectionAttemts { get; }
        public bool IsReconnectionEnabled { get; }
        public bool UseHostFromAccountAddress { get; }
        public ConnectionProxySettings? ProxySettings { get; }
        public ConnectionSecuritySettings? SecuritySettings { get; }

        ConnectionSettings()
        {
            Host = null;
            Port = 5222;
            ReconnectionAttemts = 10;
            IsReconnectionEnabled = true;
            UseHostFromAccountAddress = true;
            ProxySettings = null;
            SecuritySettings = null;
        }

        public ConnectionSettings(string? host, int port, int reconnectionAttempts, bool isReconnectionEnabled, bool useHostFromAccountAddress, ConnectionProxySettings? proxySettings, ConnectionSecuritySettings? securitySettings)
        {
            Host = host;
            Port = port;
            ReconnectionAttemts = reconnectionAttempts;
            IsReconnectionEnabled = isReconnectionEnabled;
            UseHostFromAccountAddress = useHostFromAccountAddress;
            ProxySettings = proxySettings;
            SecuritySettings = securitySettings;
        }
    }
}