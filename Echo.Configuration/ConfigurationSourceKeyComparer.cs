using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Echo.Configuration
{
    public sealed class ConfigurationSourceKeyComparer : IEqualityComparer<string>
    {
        public static readonly ConfigurationSourceKeyComparer Default = new ConfigurationSourceKeyComparer();

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