using System;

namespace Echo.Core.Capabilities
{
    public readonly struct CapabilitiesRemovedEventArgs
    {
        public XmppAddress Address { get; }
        public CapabilityCollection Capabilities { get; }

        public CapabilitiesRemovedEventArgs(XmppAddress address, CapabilityCollection capabilities)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Capabilities = capabilities ?? throw new ArgumentNullException(nameof(address));
        }
    }
}