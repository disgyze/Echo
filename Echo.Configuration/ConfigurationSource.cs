using System;

namespace Echo.Configuration
{
    public sealed class ConfigurationSource
    {
        public string Key { get; }
        public ConfigurationNode Root { get; }

        public ConfigurationSource(string key, ConfigurationNode root)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            Key = key;
            Root = root;
        }
    }
}