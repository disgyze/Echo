using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannel : IConversation
    {
        bool IsJoined { get; }
        XmppUri Address { get; }
        IChannelMember Me { get; }
        IReadOnlyList<IChannelMember> Members { get; }

        Task JoinAsync();
        Task LeaveAsync(string? reason = null);
    }
}