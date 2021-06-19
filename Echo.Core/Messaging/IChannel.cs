using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannel : IConversation
    {
        int MemberCount { get; }
        bool IsJoined { get; }
        XmppUri Address { get; }
        IChannelMember Me { get; }

        IChannelMember? GetMember(int index);
        IChannelMember? GetMember(string nick);
        IChannelMember? GetMember(XmppUri address);
        ValueTask<bool> JoinAsync(string? password = null);
        ValueTask<bool> LeaveAsync(string? reason = null);
    }
}