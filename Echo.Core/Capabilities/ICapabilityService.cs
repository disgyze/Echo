using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Client;
using Echo.Xmpp;

namespace Echo.Core.Capabilities
{
    public interface ICapabilityService
    {
        Task<CapabilityCollection> GetCapabilitiesAsync(IXmppClient connection, XmppAddress address, bool bypassCache = false, CancellationToken cancellationToken = default);
    }
}