using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public class WebSocketConnectionFactory : ConnectionFactory<WebSocketConnection>
    {
        public async ValueTask<WebSocketConnection> ConnectAsync(EndPoint endpoint, Action<ClientWebSocketOptions>? configure = null, CancellationToken cancellationToken = default)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (endpoint is not UriEndPoint)
            {
                throw new ArgumentException($"Only {typeof(UriEndPoint).FullName} is allowed", nameof(endpoint));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var uriEndpoint = (UriEndPoint)endpoint;
            var webSocket = CreateWebSocket();
            try
            {
                configure?.Invoke(webSocket.Options);
                await webSocket.ConnectAsync(uriEndpoint.Uri, cancellationToken).ConfigureAwait(false);
                return new WebSocketConnection(webSocket, uriEndpoint);
            }
            catch
            {
                webSocket.Dispose();
                throw;
            }
        }

        public override ValueTask<WebSocketConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            return ConnectAsync(endpoint, null, cancellationToken);
        }

        public ValueTask<WebSocketConnection> ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return ConnectAsync(new UriEndPoint(uri), cancellationToken);
        }

        public ValueTask<WebSocketConnection> ConnectAsync(Uri uri, Action<ClientWebSocketOptions>? configure = null, CancellationToken cancellationToken = default)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return ConnectAsync(new UriEndPoint(uri), configure, cancellationToken);
        }

        protected virtual ClientWebSocket CreateWebSocket()
        {
            return new ClientWebSocket();
        }
    }
}