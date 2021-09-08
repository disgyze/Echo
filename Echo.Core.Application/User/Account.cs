using System;

namespace Echo.Core.User
{
    public abstract class Account : IAccount
    {
        public abstract Guid Id { get; }
        public abstract XmppAddress Address { get; }
        public abstract string Password { get; set; }
        public abstract bool IsAuthenticated { get; set; }
        public abstract Presence Presence { get; set; }
    }
}