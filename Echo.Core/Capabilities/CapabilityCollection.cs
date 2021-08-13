using System;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Core.Capabilities
{
    public sealed class CapabilityCollection
    {
        IEnumerable<string> capabilities = null!;

        public CapabilityCollection(IEnumerable<string> capabilities)
        {
            this.capabilities = capabilities;
        }

        public bool Supports(string capability)
        {
            return capabilities.Any(x => string.Equals(x, capability, StringComparison.OrdinalIgnoreCase));
        }
    }
}