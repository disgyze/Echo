using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannelServiceDiscovery
    {
        ValueTask<IReadOnlyList<ChannelServiceInformation>> GetChannelServiceListAsync();
        ValueTask<IReadOnlyList<IChannel>> GetChannelRangeAsync(int min, int max);
    }
}