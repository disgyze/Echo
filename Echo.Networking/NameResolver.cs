using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public abstract class NameResolver
    {
        public abstract ValueTask<IPHostEntry> ResolveAsync(string hostOrAddress, CancellationToken cancellationToken = default);
    }
}