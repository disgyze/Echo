using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public sealed class ProxySocketConnectionFactory : ConnectionFactory<SocketConnection>
    {
        IConnectionFactory<SocketConnection> baseFactory;
        ProxyOptions options;
        ProxyKind proxyKind;

        public ProxySocketConnectionFactory(IConnectionFactory<SocketConnection> baseFactory, ProxyOptions options, ProxyKind proxyKind)
        {
            this.baseFactory = baseFactory ?? throw new ArgumentNullException(nameof(baseFactory));
            this.options = options;
            this.proxyKind = proxyKind;
        }

        public override async ValueTask<SocketConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var connection = await baseFactory.ConnectAsync(endpoint, cancellationToken).ConfigureAwait(false);
            var proxyConnection = proxyKind switch
            {
                ProxyKind.Socks4 => new Socks4ProxyConnection(options),
                _ => throw new NotSupportedException()
            };

            await proxyConnection.EstablishAsync(connection, cancellationToken).ConfigureAwait(false);
            return connection;
        }
    }
}