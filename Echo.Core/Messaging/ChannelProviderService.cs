using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ChannelProviderService
    {
        public abstract Task<IReadOnlyList<ChannelProvider>> GetChannelProvidersAsync(CancellationToken cancellationToken = default);
    }
}