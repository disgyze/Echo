using Echo.Core.Connections;

namespace Echo.Core.User
{
    public abstract class PresenceSubscriptionServiceFactory
    {
        public abstract PresenceSubscriptionService Create(XmppConnectionService connection);
    }
}