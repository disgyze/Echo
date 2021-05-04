using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SocketConnectionReader : IAsyncDisposable, IDisposable
    {
        bool disposed = false;
        Task? readerTask = null;
        SocketConnection? connection = null;
        CancellationTokenSource? cancellationTokenSource = null;

        public SocketConnectionReader(SocketConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        
        public void Dispose()
        {
            var task = DisposeAsync();

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!disposed)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    
                    if (readerTask != null)
                    {
                        await readerTask.ConfigureAwait(false);
                    }

                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    readerTask = null;
                }
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        public async ValueTask StartAsync(Action<byte[]> onData, Action<Exception>? onError = null, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (onData == null)
            {
                throw new ArgumentNullException(nameof(onData));
            }

            if (cancellationToken.IsCancellationRequested)
            {
                await ValueTask.FromCanceled(cancellationToken).ConfigureAwait(false);
            }

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            readerTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    TimeSpan delay = TimeSpan.FromMilliseconds(10);
                    Span<byte> buffer = new Span<byte>(new byte[connection!.Socket.ReceiveBufferSize]);

                    while (connection.Socket.Connected && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        int bytesRead = connection.Stream.Read(buffer);

                        if (bytesRead > 0)
                        {
                            onData(bytesRead == buffer.Length ? buffer.ToArray() : buffer.Slice(0, bytesRead).ToArray());
                        }

                        Thread.Sleep(delay);
                    }
                }
                catch (Exception e)
                {
                    onError?.Invoke(e);
                }
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await readerTask.ConfigureAwait(false);
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(SocketConnectionReader));
            }
        }
    }
}