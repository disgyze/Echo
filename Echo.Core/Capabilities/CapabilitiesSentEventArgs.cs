using System;

namespace Echo.Core.Capabilities
{
    public sealed class CapabilitiesSentEventArgs : EventArgs
    {
        public CapabilityCollection Capabilities { get; }

        public CapabilitiesSentEventArgs(CapabilityCollection capabilities)
        {
            Capabilities = capabilities;
        }
    }
}