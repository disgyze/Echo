using System;

namespace Echo.Core.Messaging
{
    public sealed class ChannelLeftEventArgs : EventArgs
    {
        public IChannel Channel { get; }
        public IChannelMember Member { get; }
        public string? Reason { get; }

        public ChannelLeftEventArgs(IChannel channel, IChannelMember member, string? reason = null)
        {
            Channel = channel;
            Member = member;
            Reason = reason;
        }
    }
}