using System;

namespace Echo.Foundation
{
    public interface IWriteOnlyServiceManager
    {
        IWriteOnlyServiceManager Register<TService>(Func<IReadOnlyServiceManager, TService> serviceProvider, ServiceCreationPolicy serviceCreationPolicy);
        IWriteOnlyServiceManager Unregister<TService>();
    }
}