using System;

namespace Echo.Core.User
{
    public interface IAccount
    {
        Guid Id { get; }
        XmppAddress Address { get; }
        string Password { get; }
        bool IsAuthenticated { get; }
        Presence Presence { get; }
    }
}