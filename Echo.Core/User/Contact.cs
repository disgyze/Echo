using System;

namespace Echo.Core.User
{
    public sealed record Contact(XmppAddress Address, XmppAddress AccountAddress, string? Nick, bool IsOnline, Presence Presence, PresenceSubscription Subscription = PresenceSubscription.None)
    {
        public XmppAddress Address { get; } = Address ?? throw new ArgumentNullException(nameof(Address));
        public XmppAddress AccountAddress { get; } = AccountAddress ?? throw new ArgumentNullException(nameof(AccountAddress));
        public Presence Presence { get; } = Presence ?? throw new ArgumentNullException(nameof(Presence));
    }
}