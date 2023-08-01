using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Echo.Configuration
{
    public sealed class ConfigurationNodeBuilder
    {
        string key;
        ImmutableList<ConfigurationNode>.Builder? nodeBuilder;
        ImmutableDictionary<string, object?>.Builder? propertyBuilder;

        public ConfigurationNodeBuilder(string key)
        {
            this.key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public ConfigurationNodeBuilder AddProperty<TValue>(string key, TValue value, TValue? defaultValue = default, IEqualityComparer<TValue>? valueComparer = default)
        {
            if (value is not null && (valueComparer is not null && !valueComparer.Equals(value, defaultValue) || !value.Equals(defaultValue)))
            {
                propertyBuilder ??= ImmutableDictionary.CreateBuilder<string, object?>();
                propertyBuilder.Add(key, value);
            }
            return this;
        }

        public ConfigurationNodeBuilder AddNode(ConfigurationNode node)
        {
            if (node is not null && !node.IsEmpty)
            {
                nodeBuilder ??= ImmutableList.CreateBuilder<ConfigurationNode>();
                nodeBuilder.Add(node);
            }
            return this;
        }

        public ConfigurationNode Build()
        {
            var properties = propertyBuilder?.ToImmutable() ?? ImmutableDictionary<string, object?>.Empty;
            var children = nodeBuilder?.ToImmutable() ?? ImmutableList<ConfigurationNode>.Empty;
            var isEmpty = key == string.Empty && properties.IsEmpty && children.IsEmpty;

            return isEmpty ? ConfigurationNode.Empty : new ConfigurationNode(key, properties, children);
        }
    }
}