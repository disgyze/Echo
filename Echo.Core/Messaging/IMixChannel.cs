using System.Collections.Generic;

namespace Echo.Core.Messaging
{
    public interface IMixChannel : IChannel
    {
        new IMixChannelMember Me { get; }
        new IReadOnlyList<IMixChannelMember> Members { get; }
    }
}