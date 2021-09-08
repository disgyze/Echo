using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public sealed class DefaultAccountManager : AccountManager
    {
        ImmutableArray<Account> accountList = ImmutableArray<Account>.Empty;
        IAccountPresenter accountPresenter;
        IAccountRegistrationPresenter accountRegistrationPresenter;

        public override int Count => accountList.Length;

        public override Account? GetAccount(int accountIndex)
        {
            var copy = accountList;
            return accountIndex >= 0 && accountIndex < copy.Length ? copy[accountIndex] : null;
        }

        public override Account? GetAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public override Account? GetAccount(XmppAddress accountAddress)
        {
            throw new NotImplementedException();
        }

        public override void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Account account)
        {
            throw new NotImplementedException();
        }

        public override void ShowAccountRegistrationUI()
        {

        }

        public override void ShowAccountUI(XmppAddress accountAddress)
        {
            throw new NotImplementedException();
        }
    }
}