using System;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public class SslSocketConnectionFactory : ConnectionFactory<SocketConnection>
    {
        IConnectionFactory<SocketConnection> baseFactory;
        SslClientAuthenticationOptions options;

        public SslSocketConnectionFactory(IConnectionFactory<SocketConnection> baseFactory, SslClientAuthenticationOptions options)
        {
            this.baseFactory = baseFactory ?? throw new ArgumentNullException(nameof(baseFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async ValueTask<SocketConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            var connection = await baseFactory.ConnectAsync(endpoint, cancellationToken).ConfigureAwait(false);
            return await SecureAsync(connection, options, cancellationToken).ConfigureAwait(false);
        }

        public static async ValueTask<SocketConnection> SecureAsync(SocketConnection connection, SslClientAuthenticationOptions options, CancellationToken cancellationToken = default)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            cancellationToken.ThrowIfCancellationRequested();

            SslStream sslStream = new SslStream(connection.Stream);
            try
            {
                await sslStream.AuthenticateAsClientAsync(options, cancellationToken).ConfigureAwait(false);
                return new SocketConnection(connection.Socket, sslStream);
            }
            catch
            {
                sslStream.Dispose();
                throw;
            }         
        }
    }
}