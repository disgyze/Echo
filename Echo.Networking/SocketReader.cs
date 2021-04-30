using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SocketReader : ISocketReader
    {
        bool disposed = false;
        Socket socket = null!;
        Stream stream = null!;
        Task task = null!;
        TaskScheduler taskScheduler = null!;
        CancellationTokenSource cancellationTokenSource = null!;

        public bool IsRunning
        {
            get;
            private set;
        }

        public SocketReader(Socket socket, Stream? stream = null)
        {
            if (socket == null) throw new ArgumentNullException(nameof(socket));

            this.socket = socket;
            this.stream = stream ?? new NetworkStream(socket);
        }
        
        ~SocketReader()
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
                    CancelAsync();
                }
            }
            disposed = true;
        }

        public ValueTask DisposeAsync()
        {
            Dispose(true);
            return default;
        }

        public Task StartAsync(Action<byte[]> onData, Action<SocketException>? onError = null)
        {
            ThrowIfDisposed();

            cancellationTokenSource = new CancellationTokenSource();

            if (cancellationTokenSource.Token.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationTokenSource.Token);
            }

            if (onData == null)
            {
                throw new ArgumentNullException(nameof(onData));
            }

            if (IsRunning)
            {
                throw new InvalidOperationException("SocketReader is alreading running.");
            }

            return task = Task.Factory.StartNew(() =>
            {
                if (cancellationTokenSource.Token.IsCancellationRequested)
                {
                    return;
                }

                IsRunning = true;
                try
                {
                    TimeSpan delay = TimeSpan.FromMilliseconds(10);
                    Span<byte> buffer = new Span<byte>(new byte[socket.ReceiveBufferSize]);

                    while (socket.Connected && socket.Available > 0 && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        int bytesRead = stream.Read(buffer);

                        if (bytesRead > 0)
                        {
                            onData(bytesRead == buffer.Length ? buffer.ToArray() : buffer.Slice(0, bytesRead).ToArray());
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
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, taskScheduler);
        }

        public Task CancelAsync()
        {
            ThrowIfDisposed();

            if (IsRunning)
            {
                cancellationTokenSource.Cancel();
                task.Wait();

                cancellationTokenSource.Dispose();
                cancellationTokenSource = null!;
                task = null!;
            }

            return Task.CompletedTask;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(SocketReader));
            }
        }
    }
}