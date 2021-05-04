using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SocketConnection : IAsyncDisposable, IDisposable
    {
        bool disposed = false;

        public Socket Socket { get; }
        public Stream Stream { get; }

        public SocketConnection(Socket socket, Stream? stream = null)
        {
            Socket = socket;
            Stream = stream ?? new NetworkStream(socket);
        }

        public void Dispose()
        {
            var task = CloseAsync(SocketConnectionCloseMethod.GracefulShutdown);

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public ValueTask DisposeAsync()
        {
            return CloseAsync(SocketConnectionCloseMethod.GracefulShutdown);
        }

        public async ValueTask CloseAsync(SocketConnectionCloseMethod closeMethod, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await ValueTask.FromCanceled(cancellationToken).ConfigureAwait(false);
            }

            if (!disposed)
            {
                if (closeMethod == SocketConnectionCloseMethod.GracefulShutdown)
                {
                    Socket.Shutdown(SocketShutdown.Both);
                }

                await Socket.DisconnectTaskAsync().ConfigureAwait(false);

                Socket.Dispose();
                Stream.Dispose();

                disposed = true;
            }
        }
    }
}