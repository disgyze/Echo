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
            Stream = stream ?? new NetworkStream(socket, true);
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

        public ValueTask CloseAsync(SocketConnectionCloseMethod closeMethod, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!disposed)
            {
                try
                {
                    if (closeMethod != SocketConnectionCloseMethod.GracefulShutdown)
                    {
                        Socket.Dispose();
                    }
                    Stream.Dispose();
                }
                catch (Exception e)
                {
                    return ValueTask.FromException(e);
                }
                finally
                {
                    disposed = true;
                }
            }

            return default;
        }
    }
}