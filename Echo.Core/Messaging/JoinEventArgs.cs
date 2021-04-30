using System;

namespace Echo.Core.Messaging
{
    public sealed class JoinEventArgs : EventArgs
    {
        public IChannel Room { get; }
        public IChannelMember Member { get; }

        public JoinEventArgs(IChannel room, IChannelMember member)
        {
            Room = room;
            Member = member;
        }
    }
}