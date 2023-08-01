using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public abstract class PresenceSubscriptionService
    {
        public abstract ValueTask<bool> SubscribeAsync(XmppAddress address, string? nick = null, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UnsubscribeAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DenySubscriptionRequestAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> ApproveSubscriptionRequestAsync(XmppAddress address, CancellationToken cancellationToken = default);
    }
}