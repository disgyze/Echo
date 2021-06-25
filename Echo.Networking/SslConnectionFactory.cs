using System;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SslConnectionFactory : ConnectionFactory
    {
        ConnectionFactory connectionFactory = null!;
        SslClientAuthenticationOptions sslOptions = null!;

        public SslConnectionFactory(ConnectionFactory connectionFactory, SslClientAuthenticationOptions sslOptions)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.sslOptions = sslOptions ?? throw new ArgumentNullException(nameof(sslOptions));
        }

        public override async ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            var connection = await connectionFactory.OpenAsync(endPoint, cancellationToken).ConfigureAwait(false);
            return await UpgradeAsync(connection, sslOptions, cancellationToken).ConfigureAwait(false);
        }

        public static async ValueTask<SocketConnection> UpgradeAsync(SocketConnection connection, SslClientAuthenticationOptions sslOptions, CancellationToken cancellationToken = default)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (sslOptions == null) throw new ArgumentNullException(nameof(sslOptions));
            cancellationToken.ThrowIfCancellationRequested();

            SslStream sslStream = new SslStream(connection.Stream);
            await sslStream.AuthenticateAsClientAsync(sslOptions, cancellationToken).ConfigureAwait(false);

            return new SocketConnection(connection.Socket, sslStream);
        }
    }
}