namespace Echo.Xmpp.Core.Ping
{
    public interface IXmppPingService : IDisposable
    {
        ValueTask<bool> PingAsync(string sender, string target, CancellationToken cancellationToken = default);
        ValueTask<bool> PongAsync(string id, string sender, string target, CancellationToken cancellationToken = default);
    }
}