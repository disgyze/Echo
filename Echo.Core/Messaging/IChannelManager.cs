using System;
using System.Threading.Tasks;
using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public interface IChannelManager
    {
        int Count { get; }
        IChannel? GetChannel(int channelIndex);
        IChannel? GetChannel(Guid channelId);
        IChannel? GetChannel(Guid connectionId, XmppUri channelAddress);
        IChannel? GetChannel(XmppUri accountAddress, XmppUri channelAddress);

        ValueTask<bool> JoinAsync(XmppUri accountAddress, XmppUri channelAddress, string? nick = null, string? password = null);
        ValueTask<bool> JoinAsync(IXmppConnection connection, XmppUri channelAddress, string? nick = null, string? password = null);
        ValueTask<bool> InviteAsync(XmppUri accountAddress, XmppUri userAddress, XmppUri channelAddress, string? reason = null);
        ValueTask<bool> InviteAsync(IXmppConnection connection, XmppUri userAddress, XmppUri channelAddress, string? reason = null);
        ValueTask<bool> KickAsync(XmppUri accountAddress, XmppUri memberAddress, string? reason = null);
        ValueTask<bool> KickAsync(XmppUri accountAddress, XmppUri channelAddrss, string nick, string? reason = null);
        ValueTask<bool> KickAsync(IXmppConnection connection, XmppUri memberAddress, string? reason = null);
        ValueTask<bool> KickAsync(IXmppConnection connection, XmppUri channelAddress, string nick, string? reason = null);
    }
}