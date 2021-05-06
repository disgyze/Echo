using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    internal abstract class ProxyConnection
    {
        public abstract ValueTask EstablishAsync(SocketConnection connection, CancellationToken cancellationToken = default);
    }
}