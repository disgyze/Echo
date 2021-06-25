using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class ProxyConnectionFactory : ConnectionFactory
    {               
        ConnectionFactory connectionFactory = null!;
        ProxyOptions options = null!;
        ProxyKind proxyKind = ProxyKind.Socks4;

        public ProxyConnectionFactory(ConnectionFactory connectionFactory, ProxyOptions options, ProxyKind proxyKind)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.proxyKind = proxyKind;
        }

        public override async ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            if (endPoint == null) throw new ArgumentNullException(nameof(endPoint));
            cancellationToken.ThrowIfCancellationRequested();

            var connection = await connectionFactory.OpenAsync(endPoint, cancellationToken).ConfigureAwait(false);
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