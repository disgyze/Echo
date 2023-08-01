using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public sealed class ConnectionStream : Stream
    {
        DuplexConnection connection;

        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override bool CanSeek => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public ConnectionStream(DuplexConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public override void Flush()
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            connection.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count)).GetAwaiter().GetResult();
        }

        public override void Write(ReadOnlySpan<byte> buffer)
        {
            base.Write(buffer);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return base.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return base.WriteAsync(buffer, cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Memory<byte> tempBuffer = new Memory<byte>(buffer, offset, count);
            ValueTask<int> readTask = connection.ReadAsync(tempBuffer);

            if (readTask.IsCompleted)
            {
                return readTask.Result;
            }
            else
            {
                return readTask.AsTask().GetAwaiter().GetResult();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}