using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface IDnsResolver : IDisposable, IAsyncDisposable
    {
        Task<IPHostEntry> ResolveAsync(string hostOrAddress, CancellationToken cancellationToken = default);
    }
}