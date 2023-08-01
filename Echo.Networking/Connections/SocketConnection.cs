using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public class SocketConnection : DuplexConnection
    {
        public override EndPoint? LocalEndpoint => Socket.LocalEndPoint;
        public override EndPoint? RemoteEndpoint => Socket.RemoteEndPoint;
        public Socket Socket { get; }
        public Stream Stream { get; }

        public SocketConnection(Socket socket, Stream? stream = null)
        {
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            Stream = stream ?? CreateStream();
        }

        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return Stream.ReadAsync(buffer, cancellationToken);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return Stream.WriteAsync(buffer, cancellationToken);
        }

        protected override ValueTask CloseAsyncCore(ConnectionCloseMethod method, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled(cancellationToken);
            }

            try
            {
                if (method != ConnectionCloseMethod.GracefulShutdown)
                {
                    Socket.Dispose();
                }
                Stream.Dispose();
            }
            catch (Exception e)
            {
                return ValueTask.FromException(e);
            }

            return default;
        }

        protected virtual Stream CreateStream()
        {
            return new NetworkStream(Socket, ownsSocket: true);
        }
    }
}