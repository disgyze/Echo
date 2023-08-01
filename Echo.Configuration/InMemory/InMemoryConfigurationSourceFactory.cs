using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Configuration.InMemory
{
    public sealed class InMemoryConfigurationSourceFactory : ConfigurationSourceFactory
    {
        Func<string, ValueTask<ConfigurationSource>> sourceFactory;

        public InMemoryConfigurationSourceFactory(Func<string, ValueTask<ConfigurationSource>> sourceFactory)
        {
            this.sourceFactory = sourceFactory ?? throw new ArgumentNullException(nameof(sourceFactory));
        }

        public override ValueTask<ConfigurationSource> LoadAsync(string sourceKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            return sourceFactory(sourceKey);
        }
    }
}