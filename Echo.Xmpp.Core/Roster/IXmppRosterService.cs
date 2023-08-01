using Echo.Xmpp.ElementModel;

namespace Echo.Xmpp.Core.Roster
{
    public interface IXmppRosterService : IDisposable
    {
        ValueTask<XmppIQ?> GetRosterAsync(string? version = null, CancellationToken cancellationToken = default);
        ValueTask AcceptAsync(string target, CancellationToken cancellationToken = default);
        ValueTask DeclineAsync(string target, CancellationToken cancellationToken = default);
    }
}