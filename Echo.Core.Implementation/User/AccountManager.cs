using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public sealed class AccountManager : IAccountManager
    {
        ImmutableArray<IAccount> accountList = ImmutableArray<IAccount>.Empty;

        public int Count => accountList.Length;

        public IAccount? CreateAccount(XmppAddress address, string? password = null)
        {
            return new Account(address, password, false, false, false, new Presence());
        }

        public IAccount? GetAccount(int accountIndex)
        {
            throw new NotImplementedException();
        }

        public IAccount? GetAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public IAccount? GetAccount(XmppAddress accountAddress)
        {
            throw new NotImplementedException();
        }

        public IAccount? UpdateAccount(IAccount account)
        {
            var existingAccount = accountList.FirstOrDefault(otherAccount => otherAccount.Id == account.Id);

            if (existingAccount != null)
            {
                ImmutableInterlocked.Update(ref accountList, accountList => accountList.Replace(existingAccount, account));
                return account;
            }

            return null;
        }

        public void ShowAccountRegistrationUI()
        {
            throw new NotImplementedException();
        }

        public void ShowAccountUI(XmppAddress accountAddress)
        {
            throw new NotImplementedException();
        }
    }
}