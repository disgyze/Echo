using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Capabilities
{
    public abstract class CapabilityCache
    {
        public abstract ValueTask<CapabilityCollection?> GetCapabilitiesAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask SaveOrUpdateCapabilitiesAsync(XmppAddress address, CapabilityCollection capabilities, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteCapabilitiesAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> ClearAsync(CancellationToken cancellationToken = default);
    }
}