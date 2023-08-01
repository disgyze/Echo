using System;

namespace Echo.Core.Capabilities
{
    public readonly struct CapabilitiesAddedEventArgs
    {
        public XmppAddress Address { get; }
        public CapabilityCollection Capabilities { get; }
        
        public CapabilitiesAddedEventArgs(XmppAddress address, CapabilityCollection capabilities)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Capabilities = capabilities ?? throw new ArgumentNullException(nameof(capabilities));
        }
    }
}