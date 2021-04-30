using System;
using System.Collections.Immutable;

namespace Echo.Foundation
{
    public sealed class DisposableContainer : IDisposable
    {
        bool disposed = false;
        ImmutableArray<IDisposable> innerList = ImmutableArray<IDisposable>.Empty;

        public DisposableContainer(params IDisposable[] items)
        {
            innerList = ImmutableArray.Create(items);
        }

        ~DisposableContainer()
        {
            Dispose(false);
        }

        public void Add(IDisposable item)
        {
            ImmutableInterlocked.InterlockedExchange(ref innerList, innerList.Add(item));
        }

        public void AddRange(params IDisposable[] items)
        {
            ImmutableInterlocked.InterlockedExchange(ref innerList, innerList.AddRange(items));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var item in innerList)
                    {
                        item.Dispose();
                    }
                    ImmutableInterlocked.InterlockedExchange(ref innerList, innerList.Clear());
                }
                disposed = true;
            }
        }
    }
}