using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.Configuration.User
{
    public sealed class UserSettings
    {
        public IReadOnlyList<AccountSettings> Accounts { get; }
    }
}