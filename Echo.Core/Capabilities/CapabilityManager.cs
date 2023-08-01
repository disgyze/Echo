using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Capabilities
{
    public abstract class CapabilityManager
    {
        public abstract ValueTask<CapabilityCollection?> GetCapabilitiesFromCacheAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask<CapabilityCollection?> GetCapabilitiesFromNetworkAsync(Guid connectionId, XmppAddress address, CancellationToken cancellationToken = default);
    }
}