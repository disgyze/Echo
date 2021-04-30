using System;

namespace Echo.Foundation
{
    public interface IWriteOnlyServiceManager
    {
        IWriteOnlyServiceManager Register<TService>(Func<IReadOnlyServiceManager, TService> serviceFactory, ServiceCreationPolicy serviceCreationPolicy = ServiceCreationPolicy.Instance);
        IWriteOnlyServiceManager Unregister<TService>();
    }
}