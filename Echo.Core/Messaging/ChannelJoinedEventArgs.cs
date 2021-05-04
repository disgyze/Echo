using System;

namespace Echo.Core.Messaging
{
    public sealed class ChannelJoinedEventArgs : EventArgs
    {
        public IChannel Channel { get; }
        public IChannelMember Member { get; }

        public ChannelJoinedEventArgs(IChannel channel, IChannelMember member)
        {
            Channel = channel;
            Member = member;
        }
    }
}