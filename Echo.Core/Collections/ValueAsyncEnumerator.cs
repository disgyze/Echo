using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Collections
{
    public struct ValueAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        T? current = default;
        bool done = true;
        int index = 0;
        int rangeLength = 0;
        int limit = 25;
        int offset = 0;
        ReadOnlyRange<T> range = default;
        CancellationToken cancellationToken;
        ValueAsyncEnumeratorAction<T> action;

        public T Current => current!;

        public ValueAsyncEnumerator(ValueAsyncEnumeratorAction<T> action, CancellationToken cancellationToken = default)
        {
            this.action = action;
            this.cancellationToken = cancellationToken;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            if (done)
            {
                range = await action(offset, limit, cancellationToken).ConfigureAwait(false);

                if (range.IsEmpty)
                {
                    return false;
                }

                done = false;
                index = 0;
                offset += range.Count;
                rangeLength = range.Count;
            }

            current = range[index];
            index++;

            if (index == rangeLength)
            {
                done = true;
            }

            return true;
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}