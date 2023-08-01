using System;
using System.Collections.Generic;

namespace Echo.Core.Collections
{
    public readonly struct ReadOnlyRange<T>
    {
        public struct Enumerator
        {
            IReadOnlyList<T> source;
            int index;
            int count;

            public T Current => source[index];

            public Enumerator(IReadOnlyList<T> source, int index, int count)
            {
                this.source = source;
                this.index = --index;
                this.count = count;
            }

            public bool MoveNext()
            {
                index++;
                return count-- > 0;
            }
        }

        readonly IReadOnlyList<T> source;
        readonly int startAt;
        readonly int count;

        public int Count => count;
        public bool IsEmpty => count == 0;
        public T this[int index] => source[index + startAt];

        public static readonly ReadOnlyRange<T> Empty = new ReadOnlyRange<T>();

        public ReadOnlyRange(IReadOnlyList<T> source, int startAt, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startAt < 0 || source.Count < startAt)
            {
                throw new ArgumentOutOfRangeException(nameof(startAt));
            }

            if (count <= 0 || source.Count < count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            this.source = source;
            this.startAt = startAt;
            this.count = count;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(source, startAt, count);
        }
    }
}