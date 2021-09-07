namespace Echo.Core.Messaging
{
    public interface IChannelManager : IChannelService
    {
        void Add(IChannel channel);
        void Remove(IChannel channel);
    }
}