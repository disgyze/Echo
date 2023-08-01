using System;
using System.Collections.Immutable;
using System.Linq;

namespace Echo.Core.Capabilities
{
    public sealed class CapabilityCollection : IEquatable<CapabilityCollection>
    {
        ImmutableArray<string> capabilities;

        public CapabilityCollection(ImmutableArray<string> capabilities)
        {
            this.capabilities = capabilities;
        }

        public bool HasCapability(string capability)
        {
            return capabilities.Any(otherCapability => string.Equals(otherCapability, capability, StringComparison.OrdinalIgnoreCase));
        }

        public bool Equals(CapabilityCollection? otherCapabilities)
        {
            return ReferenceEquals(this, otherCapabilities) || (otherCapabilities is not null && capabilities.SequenceEqual(otherCapabilities.capabilities, StringComparer.OrdinalIgnoreCase));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CapabilityCollection);
        }

        public override int GetHashCode()
        {
            return capabilities.GetHashCode();
        }

        public ImmutableArray<string>.Enumerator GetEnumerator()
        {
            return capabilities.GetEnumerator();
        }
    }
}