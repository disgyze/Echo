using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IChannelDiscoveryService
    {
        Task<IEnumerable<IChannel>> GetChannelRangeAsync(int limit = 25, int offset = 0);
    }
}