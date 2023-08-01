using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public abstract class DuplexConnection : Connection, IReadableConnection, IWritableConnection
    {
        public abstract ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);
        public abstract ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
    }
}