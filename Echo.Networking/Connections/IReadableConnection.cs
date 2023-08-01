using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public interface IReadableConnection
    {
        ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);
    }
}