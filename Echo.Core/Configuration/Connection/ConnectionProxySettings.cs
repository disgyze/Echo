namespace Echo.Core.Configuration.Connection
{
    public sealed class ConnectionProxySettings
    {
        public string Address { get; }
        public int Port { get; }
        public int ProxyKind { get; }
        public ProxyCredential? Credential { get; }

        public ConnectionProxySettings(string address, int port, int proxyKind, ProxyCredential? credential)
        {
            Address = address;
            Port = port;
            ProxyKind = proxyKind;
            Credential = credential;
        }
    }
}