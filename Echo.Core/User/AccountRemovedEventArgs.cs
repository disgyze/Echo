namespace Echo.Core.User
{
    public readonly struct AccountRemovedEventArgs
    {
        public Account Account { get; }

        public AccountRemovedEventArgs(Account account)
        {
            Account = account;
        }
    }
}