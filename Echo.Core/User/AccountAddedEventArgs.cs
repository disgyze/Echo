namespace Echo.Core.User
{
    public readonly struct AccountAddedEventArgs
    {
        public Account Account { get; }

        public AccountAddedEventArgs(Account account)
        {
            Account = account;
        }
    }
}