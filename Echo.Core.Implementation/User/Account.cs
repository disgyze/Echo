using System;

namespace Echo.Core.User
{
    public sealed class Account : IAccount
    {
        public Guid Id { get; }
        public XmppAddress Address { get; }
        public string? Password { get; }
        public bool IsAuthenticated { get; }
        public bool IsMetaAccount { get; }
        public bool IsLinked { get; }
        public Presence Presence { get; }

        public Account(XmppAddress addres, string? password, bool isAuthenticated, bool isMetaAccount, bool isLinked, Presence presence)
        {
            Id = Guid.NewGuid();
            Address = addres;
            Password = password;
            IsAuthenticated = isAuthenticated;
            IsMetaAccount = isMetaAccount;
            IsLinked = isLinked;
            Presence = presence;
        }

        public IAccount WithPresence(Presence presence)
        {
            return new Account(this.Address, this.Password, this.IsAuthenticated, this.IsMetaAccount, this.IsLinked, presence);
        }

        public IAccount WithPassword(string? password)
        {
            return new Account(this.Address, password, this.IsAuthenticated, this.IsMetaAccount, this.IsLinked, this.Presence);
        }
    }
}