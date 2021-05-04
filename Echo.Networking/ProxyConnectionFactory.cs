using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed partial class ProxyConnectionFactory : ConnectionFactory
    {
        ConnectionFactory connectionFactory = null!;
        ProxyOptions options = null!;

        public ProxyConnectionFactory(ConnectionFactory connectionFactory, ProxyOptions options)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}