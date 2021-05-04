using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Client;

namespace Echo.Core.Capabilities
{
    public interface ICapabilityService
    {
        Task<CapabilityCollection> GetCapabilitiesAsync(IXmppClient connection, XmppUri address, bool bypassCache = false, CancellationToken cancellationToken = default);
    }
}