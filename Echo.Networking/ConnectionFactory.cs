using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public abstract class ConnectionFactory
    {
        public abstract ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default);
    }
}