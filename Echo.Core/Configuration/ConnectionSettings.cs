using System;

namespace Echo.Core.Configuration
{
    public sealed record ConnectionSettings(Guid AccountId, string? Host, int Port, bool UseHostFromAccountAddress, TimeSpan Timeout, ConnectionRetryPolicy RetryPolicy, ConnectionProxySettings Proxy, ConnectionSecuritySettings Security)
    {
        public static readonly ConnectionSettings Default = new ConnectionSettings(
            AccountId: Guid.Empty,
            Host: string.Empty,
            Port: 5222,
            UseHostFromAccountAddress: true,
            Timeout: TimeSpan.FromSeconds(30),
            RetryPolicy: ConnectionRetryPolicy.Default,
            Proxy: ConnectionProxySettings.Default,
            Security: ConnectionSecuritySettings.Default);
    }
}