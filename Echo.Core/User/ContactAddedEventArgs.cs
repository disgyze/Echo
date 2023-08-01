namespace Echo.Core.User
{
    public readonly struct ContactAddedEventArgs
    {
        public Contact Contact { get; }

        public ContactAddedEventArgs(Contact contact)
        {
            Contact = contact;
        }
    }
}