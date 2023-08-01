using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public sealed class WebSocketConnection : DuplexConnection
    {
        public override EndPoint? LocalEndpoint => null;
        public override EndPoint? RemoteEndpoint { get; }
        public WebSocket Socket { get; }

        public WebSocketConnection(WebSocket socket, UriEndPoint remoteEndpoint)
        {
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            RemoteEndpoint = remoteEndpoint ?? throw new ArgumentNullException(nameof(remoteEndpoint));
        }   

        protected override ValueTask CloseAsyncCore(ConnectionCloseMethod closeMethod, CancellationToken cancellationToken = default)
        {
            return new ValueTask(Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken));
        }

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            var result = await Socket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
            return result.Count;
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return Socket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken);
        }

        public ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken = default)
        {
            return Socket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
        }
    }
}