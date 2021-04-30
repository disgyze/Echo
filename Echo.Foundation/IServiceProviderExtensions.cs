using System;

namespace Echo.Foundation
{
    public static class IServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            return (TService)serviceProvider.GetService(typeof(TService));
        }
    }
}