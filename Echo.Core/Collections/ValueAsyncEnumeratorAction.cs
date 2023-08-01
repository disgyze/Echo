using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Collections
{
    public delegate ValueTask<ReadOnlyRange<T>> ValueAsyncEnumeratorAction<T>(int offset, int limit, CancellationToken cancellationToken = default);
}