using System;

namespace Echo.Core.User
{
    public sealed record Account(XmppAddress Address, string? Nick, bool IsOnline, Presence Presence)
    {
        public XmppAddress Address { get; } = Address ?? throw new ArgumentNullException(nameof(Address));
        public Presence Presence { get; } = Presence ?? throw new ArgumentNullException(nameof(Presence));
    }
}