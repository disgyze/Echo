namespace Echo.Core.Configuration.Connection
{
    public sealed class ConnectionProxySettings
    {
        public string Host { get; }
        public int Port { get; }
        public int ProxyKind { get; }
        public ProxyCredential? Credential { get; }

        public ConnectionProxySettings(string host, int port, int proxyKind, ProxyCredential? credential)
        {
            Host = host;
            Port = port;
            ProxyKind = proxyKind;
            Credential = credential;
        }
    }
}