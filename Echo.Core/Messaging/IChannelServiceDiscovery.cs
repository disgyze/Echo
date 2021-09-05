using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannelServiceDiscovery
    {
        Task<IReadOnlyList<ChannelServiceInformation>> GetChannelServiceListAsync();
        Task<IReadOnlyList<IChannel>> GetChannelRangeAsync(int min, int max);
    }
}