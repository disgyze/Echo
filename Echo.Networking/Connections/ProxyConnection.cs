using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    internal abstract class ProxyConnection
    {
        public abstract ValueTask EstablishAsync(SocketConnection connection, CancellationToken cancellationToken = default);
    }
}