using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Configuration
{
    public abstract class ConfigurationManager
    {
        public abstract TSettings GetSettings<TSettings>();
        public abstract bool UpdateSettings<TSettings>(TSettings settings);
        public abstract ValueTask LoadAsync(CancellationToken cancellationToken = default);
        public abstract ValueTask SaveAsync(CancellationToken cancellationToken = default);
    }
}