namespace Echo.Core.User
{
    public readonly struct AccountChangedEventArgs
    {
        public Account NewAccount { get; }
        public Account OldAccount { get; }
        public AccountChangeKind ChangeKind { get; }

        public AccountChangedEventArgs(Account newAccount, Account oldAccount, AccountChangeKind changeKind)
        {
            NewAccount = newAccount;
            OldAccount = oldAccount;
            ChangeKind = changeKind;
        }
    }
}