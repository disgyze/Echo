using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SocketConnectionWriter : IAsyncDisposable, IDisposable
    {
        bool disposed = false;
        Socket? socket = null;
        Stream? stream = null;
        CancellationTokenSource? cancellationTokenSource = null;

        public bool IsRunning
        {
            get;
            private set;
        }

        public SocketConnectionWriter(Socket socket, Stream? stream = null)
        {
            if (socket == null) throw new ArgumentNullException(nameof(socket));

            this.socket = socket;
            this.stream = stream ?? new NetworkStream(socket);
        }

        ~SocketConnectionWriter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Cancel();
                    socket = null;
                    stream = null;
                }
            }
            disposed = true;
        }

        public ValueTask DisposeAsync()
        {
            Dispose(true);
            return default;
        }

        public void Start(ConcurrentQueue<ReadOnlyMemory<byte>> queue, Action<SocketException>? onError = null)
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));

            _ = Task.Factory.StartNew(() =>
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                IsRunning = true;
                try
                {
                    TimeSpan delay = TimeSpan.FromMilliseconds(10);

                    while (socket.Connected && !cancellationTokenSource.IsCancellationRequested)
                    {
                        while (queue.Count > 0 && !cancellationTokenSource.IsCancellationRequested)
                        {
                            if (queue.TryDequeue(out var data))
                            {
                                stream.Write(data.Span);
                                stream.Flush();
                            }
                        }
                        Thread.Sleep(delay);
                    }
                }
                catch (SocketException e)
                {
                    onError?.Invoke(e);
                }
                finally
                {
                    IsRunning = false;
                }
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Cancel()
        {
            ThrowIfDisposed();

            if (!IsRunning)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(SocketConnectionWriter));
            }
        }
    }
}