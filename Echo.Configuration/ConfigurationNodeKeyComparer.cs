using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Echo.Configuration
{
    public sealed class ConfigurationNodeKeyComparer : IEqualityComparer<string>
    {
        public static readonly ConfigurationNodeKeyComparer Default = new ConfigurationNodeKeyComparer();

        public bool Equals(string? x, string? y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return obj.GetHashCode();
        }
    }
}