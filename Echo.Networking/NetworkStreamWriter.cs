﻿using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class NetworkStreamWriter : IAsyncDisposable, IDisposable
    {
        bool disposed = false;
        NetworkStream? stream = null;
        BlockingCollection<byte[]>? queue = null;
        CancellationTokenSource? cancellationTokenSource = null;

        public NetworkStreamWriter(NetworkStream stream)
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

                if (queue != null)
                {
                    queue.Dispose();
                    queue = null;
                }

                disposed = true;
            }

            GC.SuppressFinalize(this);
            return default;
        }

        public Task StartAsync(Action<Exception>? onError = null, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    while (stream!.Socket.Connected && !cancellationTokenSource.IsCancellationRequested)
                    {
                        byte[] data = queue!.Take();

                        if (data != null && data.Length > 0)
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }
                catch (Exception e)
                {
                    onError?.Invoke(e);
                }
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Write(byte[] data)
        {
            ThrowIfDisposed();
            queue!.Add(data);
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(NetworkStreamWriter));
            }
        }
    }
}