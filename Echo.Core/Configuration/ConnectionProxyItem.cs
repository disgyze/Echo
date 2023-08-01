namespace Echo.Core.Configuration
{
    public sealed record ConnectionProxyItem(string Host, int Port, ConnectionProxyProtocol Protocol, ConnectionProxyCredential? Credential = null);
}