using System;
using System.Buffers;
using System.Threading;

namespace Echo.Xmpp.Parser
{
    internal struct Buffer<T> : IDisposable
    {
        T[] buffer;
        int written;
        int disposed = 0;

        const int MinimumBufferSize = 1024;

        public ReadOnlyMemory<T> WrittenMemory => buffer.AsMemory(0, written);

        public Buffer() : this(MinimumBufferSize)
        {
        }

        public Buffer(int initialSize)
        {
            buffer = ArrayPool<T>.Shared.Rent(initialSize > 0 ? initialSize : MinimumBufferSize);
            written = 0;
        }

        public Buffer(ReadOnlySpan<T> data) : this(data.Length)
        {
            data.CopyTo(buffer);
            written = data.Length;
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0)
            {
                return;
            }
            ArrayPool<T>.Shared.Return(buffer, true);
        }

        public void Write(ReadOnlySpan<T> data)
        {
            ThrowIfDisposed();

            int availableSpace = buffer.Length - written;

            if (availableSpace < data.Length)
            {
                Grow(data.Length);
            }

            data.CopyTo(buffer.AsSpan(written));
            written += data.Length;
        }

        private void Grow(int sizeHint)
        {
            T[] oldBuffer = buffer;
            int newSize = oldBuffer.Length + sizeHint;

            buffer = ArrayPool<T>.Shared.Rent(newSize);
            oldBuffer.AsSpan(0, written).CopyTo(buffer);
            ArrayPool<T>.Shared.Return(oldBuffer, true);
        }
        
        private void ThrowIfDisposed()
        {
            if (disposed == 1)
            {
                throw new ObjectDisposedException(nameof(Buffer<T>));
            }
        }
    }
}