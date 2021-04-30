using System.Collections.Generic;

namespace Echo.Core.Messaging
{
    public interface IMixChannel : IChannel
    {
        new IReadOnlyList<IMixChannelMember> Members { get; }
    }
}