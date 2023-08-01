using System.Collections.Generic;
using Echo.Core.Collections;

namespace Echo.Core
{
    public static class IReadOnlyListExtensions
    {
        public static ReadOnlyRange<T> GetReadOnlyRange<T>(this IReadOnlyList<T> source, int startAt, int count)
        {
            return new ReadOnlyRange<T>(source, startAt, count);
        }
    }
}