namespace Echo.Core.User
{
    public readonly struct ContactRemovedEventArgs
    {
        public Contact Contact { get; }

        public ContactRemovedEventArgs(Contact contact)
        {
            Contact = contact;
        }
    }
}