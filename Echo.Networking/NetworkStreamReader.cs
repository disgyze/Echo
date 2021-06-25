using System;
using System.Buffers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class NetworkStreamReader : IAsyncDisposable, IDisposable
    {
        bool disposed = false;
        NetworkStream? stream = null;
        CancellationTokenSource? cancellationTokenSource = null;

        public NetworkStreamReader(NetworkStream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
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

        public ValueTask DisposeAsync()
        {
            if (!disposed)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }
                disposed = true;
            }

            GC.SuppressFinalize(this);
            return default;
        }

        public Task StartAsync(Action<byte[]> onData, Action<Exception>? onError = null, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (onData == null) throw new ArgumentNullException(nameof(onData));
            cancellationToken.ThrowIfCancellationRequested();

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            return Task.Factory.StartNew(() =>
            {
                byte[] data = ArrayPool<byte>.Shared.Rent(stream!.Socket.ReceiveBufferSize);
                Span<byte> buffer = new Span<byte>(ArrayPool<byte>.Shared.Rent(stream!.Socket.ReceiveBufferSize));
                try
                {                    
                    while (stream.Socket.Connected && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        int bytesRead = stream.Read(buffer);

                        if (bytesRead > 0)
                        {
                            onData(bytesRead == buffer.Length ? buffer.ToArray() : buffer.Slice(0, bytesRead).ToArray());
                        }
                    }
                }
                catch (Exception e)
                {
                    onError?.Invoke(e);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(data);
                }
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(NetworkStreamReader));
            }
        }
    }
}