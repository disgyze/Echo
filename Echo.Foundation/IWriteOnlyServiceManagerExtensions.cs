using System;

namespace Echo.Foundation
{
    public static class IWriteOnlyServiceManagerExtensions
    {
        public static IWriteOnlyServiceManager RegisterSingleton<TService>(this IWriteOnlyServiceManager serviceManager, Func<IReadOnlyServiceManager, TService> serviceProvider)
        {
            return serviceManager.Register(serviceProvider, ServiceCreationPolicy.Singleton);
        }

        public static IWriteOnlyServiceManager RegisterInstance<TService>(this IWriteOnlyServiceManager serviceManager, Func<IReadOnlyServiceManager, TService> serviceProvider)
        {
            return serviceManager.Register(serviceProvider, ServiceCreationPolicy.Singleton);
        }
    }
}