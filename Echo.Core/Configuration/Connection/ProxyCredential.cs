using System;

namespace Echo.Core.Configuration.Connection
{
    public sealed class ProxyCredential
    {
        public string UserName { get; }
        public string Password { get; }

        public ProxyCredential(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }

            UserName = userName;
            Password = password;
        }
    }
}