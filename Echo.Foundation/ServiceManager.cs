using System;
using System.Collections.Generic;

namespace Echo.Foundation
{
    public sealed class ServiceManager : IServiceManager
    {
        Dictionary<Type, Func<object>> map = new Dictionary<Type, Func<object>>();

        public TService GetService<TService>()
        {
            var serviceType = typeof(TService);

            if (!map.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Unknown service type: {serviceType}.");
            }

            return (TService)map[serviceType]();
        }

        public IWriteOnlyServiceManager Register<TService>(Func<IReadOnlyServiceManager, TService> serviceFactory, ServiceCreationPolicy serviceCreationPolicy = ServiceCreationPolicy.Instance)
        {
            Type serviceType = typeof(TService);
            Func<object> initializer = null;

            switch (serviceCreationPolicy)
            {
                case ServiceCreationPolicy.Instance:
                {
                    initializer = () => serviceFactory(this);
                    break;
                }

                case ServiceCreationPolicy.Singleton:
                {
                    var singletonInitializer = new Lazy<object>(() => serviceFactory(this));
                    initializer = () => singletonInitializer.Value;
                    break;
                }

                default:
                    throw new NotImplementedException();
            }

            if (map.ContainsKey(serviceType))
            {
                map[serviceType] = initializer;
            }
            else
            {
                map.Add(serviceType, initializer);
            }

            return this;
        }

        public IWriteOnlyServiceManager Unregister<TService>()
        {
            var serviceType = typeof(TService);

            if (map.ContainsKey(typeof(TService)))
            {
                map.Remove(serviceType);
            }

            return this;
        }
    }
}