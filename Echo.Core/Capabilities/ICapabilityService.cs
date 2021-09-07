using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Connections;

namespace Echo.Core.Capabilities
{
    public interface ICapabilityService
    {
        ValueTask<CapabilityCollection> GetCapabilitiesAsync(IXmppConnection connection, XmppAddress address, bool bypassCache = false, CancellationToken cancellationToken = default);
    }
}