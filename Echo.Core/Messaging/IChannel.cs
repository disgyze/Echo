using System.Collections.Generic;
using Echo.Xmpp;

namespace Echo.Core.Messaging
{
    public interface IChannel : IConversation
    {
        bool IsJoined { get; }
        XmppAddress Address { get; }
        IChannelMember Me { get; }
        IReadOnlyList<IChannelMember> Members { get; }
    }
}