using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Connections;

namespace Echo.Core.Capabilities
{
    public interface ICapabilityService
    {
        Task<CapabilityCollection> GetCapabilitiesAsync(IXmppConnection connection, XmppUri address, bool bypassCache = false, CancellationToken cancellationToken = default);
    }
}