using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannelManager
    {
        IReadOnlyList<IChannel> Channels { get; }

        Task JoinChannelAsync(XmppUri address, CancellationToken cancellationToken = default);
        Task LeaveChannelAsync(XmppUri address, CancellationToken cancellationToken = default);
    }
}