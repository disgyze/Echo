using System;

namespace Echo.Core.Messaging
{
    public sealed class LeftEventArgs : EventArgs
    {
        public IChannel Room { get; }
        public IChannelMember Member { get; }
        public string? Reason { get; }

        public LeftEventArgs(IChannel room, IChannelMember member, string? reason = null)
        {
            Room = room;
            Member = member;
            Reason = reason;
        }
    }
}