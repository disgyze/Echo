using System;
using System.Collections.Generic;

namespace Echo.Core.Capabilities
{
    public sealed class CapabilityCollection
    {
        List<string> capabilities = null!;

        public CapabilityCollection(IEnumerable<string> capabilities)
        {
            this.capabilities = new List<string>(capabilities ?? throw new ArgumentNullException(nameof(capabilities)));
        }

        public bool Supports(string capability)
        {
            return capabilities.Contains(capability);
        }
    }
}