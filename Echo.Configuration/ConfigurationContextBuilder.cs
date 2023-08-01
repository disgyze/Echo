using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Configuration
{
    public sealed class ConfigurationContextBuilder
    {
        ImmutableDictionary<string, ConfigurationSourceFactory> sourceFactoryMap;

        public ConfigurationContextBuilder(ImmutableDictionary<string, ConfigurationSourceFactory>? sourceFactories = null)
        {
            sourceFactoryMap = sourceFactories ?? ImmutableDictionary<string, ConfigurationSourceFactory>.Empty;
        }

        public ConfigurationContextBuilder Add(string sourceKey, ConfigurationSourceFactory sourceFactory, bool throwIfKeyExists = true)
        {
            if (!TryAdd(sourceKey, sourceFactory) && throwIfKeyExists)
            {
                throw new ArgumentException("The key is already exist", nameof(sourceKey));
            }
            return this;
        }

        public bool TryAdd(string sourceKey, ConfigurationSourceFactory sourceFactory)
        {
            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            if (sourceFactory is null)
            {
                throw new ArgumentNullException(nameof(sourceFactory));
            }

            return ImmutableInterlocked.TryAdd(ref sourceFactoryMap, sourceKey, sourceFactory);
        }

        public async ValueTask<ConfigurationContext> BuildAsync(Action<string, Exception>? onError = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var map = sourceFactoryMap;

            if (map.Count > 0)
            {
                var sourceBuilder = ImmutableList.CreateBuilder<ConfigurationSource>();

                foreach (var (sourceKey, sourceFactory) in map)
                {
                    try
                    {
                        sourceBuilder.Add(await sourceFactory.LoadAsync(sourceKey, cancellationToken).ConfigureAwait(false));
                    }
                    catch (Exception e) when (e is not OperationCanceledException)
                    {
                        onError?.Invoke(sourceKey, e);
                    }
                }

                return new ConfigurationContext(sourceBuilder.ToImmutable());
            }

            return ConfigurationContext.Empty;
        }
    }
}