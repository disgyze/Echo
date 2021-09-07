using System;

namespace Echo.Core.Messaging
{
    public interface IChannelService
    {
        int Count { get; }

        IChannel? GetChannel(int channelIndex);
        IChannel? GetChannel(Guid channelId);
        IChannel? GetChannel(XmppAddress channelAddress);
    }
}