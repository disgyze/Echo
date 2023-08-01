using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public abstract class ChannelProviderServiceFactory
    {
        public abstract ChannelProviderService Create(XmppConnectionService connection);
    }
}