using System;
using System.Collections.Generic;

namespace Echo.Foundation
{
    public sealed class DisposableContainer : IDisposable
    {
        bool disposed = false;
        IEnumerable<IDisposable> innerList = null;

        public DisposableContainer(params IDisposable[] items)
        {
            innerList = items;
        }

        ~DisposableContainer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var item in innerList)
                {
                    item.Dispose();
                }
            }

            disposed = true;
        }
    }
}