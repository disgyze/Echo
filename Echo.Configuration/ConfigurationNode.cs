using System;
using System.Collections.Immutable;

namespace Echo.Configuration
{
    public sealed record ConfigurationNode
    {
        public static readonly ConfigurationNode Empty = new ConfigurationNode(string.Empty);

        ImmutableList<ConfigurationNode> children;
        ImmutableDictionary<string, object?> properties;

        public string Key
        {
            get;
        }

        public bool IsEmpty
        {
            get => children.Count == 0 && properties.Count == 0;
        }

        public ImmutableList<ConfigurationNode> Children
        {
            get => children;
            init => children = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ImmutableDictionary<string, object?> Properties
        {
            get => properties;
            init => properties = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ConfigurationNode(string Key, ImmutableDictionary<string, object?>? Properties = default, ImmutableList<ConfigurationNode>? Children = default)
        {
            if (Key is null)
            {
                throw new ArgumentNullException(nameof(Key));
            }

            this.Key = Key;
            properties = Properties ?? ImmutableDictionary<string, object?>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);
            children = Children ?? ImmutableList<ConfigurationNode>.Empty;
        }
    }
}