using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public interface IWritableConnection
    {
        ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
    }
}