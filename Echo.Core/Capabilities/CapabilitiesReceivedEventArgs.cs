using System;

namespace Echo.Core.Capabilities
{
    public sealed class CapabilitiesReceivedEventArgs : EventArgs
    {
        public CapabilityCollection Capabilities { get; }

        public CapabilitiesReceivedEventArgs(CapabilityCollection capabilities)
        {
            Capabilities = capabilities;
        }
    }
}