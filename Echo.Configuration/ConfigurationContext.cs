using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Echo.Configuration
{
    public sealed class ConfigurationContext
    {
        public static readonly ConfigurationContext Empty = new ConfigurationContext(ImmutableList<ConfigurationSource>.Empty);

        ImmutableList<ConfigurationSource> sourceCollection;

        public IEnumerable<ConfigurationSource> Sources => sourceCollection;

        public ConfigurationContext(ImmutableList<ConfigurationSource> sources)
        {
            sourceCollection = sources ?? ImmutableList<ConfigurationSource>.Empty;
        }

        public ConfigurationNode GetRoot(string sourceKey)
        {
            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            if (sourceCollection.Find(source => ConfigurationSourceKeyComparer.Default.Equals(source.Key, sourceKey)) is ConfigurationSource source)
            {
                return source.Root;
            }

            return ConfigurationNode.Empty;
        }

        public ConfigurationNode GetRootOrAdd(string sourceKey, ConfigurationNode root)
        {
            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            ConfigurationNode node = root;

            ImmutableInterlocked.Update(ref sourceCollection, (collection, state) =>
            {
                if (collection.Find(source => ConfigurationSourceKeyComparer.Default.Equals(source.Key, state.sourceKey)) is ConfigurationSource source)
                {
                    node = source.Root;
                    return collection;
                }
                return collection.Add(new ConfigurationSource(state.sourceKey, state.root));
            }, (sourceKey, root));

            return node;
        }

        public bool UpdateRoot(string sourceKey, ConfigurationNode root)
        {
            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            return ImmutableInterlocked.Update(ref sourceCollection, static (collection, state) =>
            {
                if (collection.Find(source => ConfigurationSourceKeyComparer.Default.Equals(source.Key, state.sourceKey)) is ConfigurationSource oldSource)
                {
                    return collection.Replace(oldSource, new ConfigurationSource(state.sourceKey, state.root));
                }
                return collection;
            }, (sourceKey, root));
        }
    }
}