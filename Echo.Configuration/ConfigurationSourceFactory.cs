using System.Threading;
using System.Threading.Tasks;

namespace Echo.Configuration
{
    public abstract class ConfigurationSourceFactory
    {
        public abstract ValueTask<ConfigurationSource> LoadAsync(string sourceKey, CancellationToken cancellationToken = default);
    }
}