using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public abstract class ConnectionFactory : IConnectionFactory
    {
        public abstract ValueTask<IConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
    }

    public abstract class ConnectionFactory<TConnection> : IConnectionFactory where TConnection : IConnection
    {
        public abstract ValueTask<TConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
        async ValueTask<IConnection> IConnectionFactory.ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken) => await ConnectAsync(endpoint, cancellationToken).ConfigureAwait(false);
    }
}