namespace Echo.Core.User
{
    public readonly struct ContactChangedEventArgs
    {
        public Contact NewContact { get; }
        public Contact OldContact { get; }
        public ContactChangeKind ChangeKind { get; }

        public ContactChangedEventArgs(Contact newContact, Contact oldContact, ContactChangeKind changeKind)
        {
            NewContact = newContact;
            OldContact = oldContact;
            ChangeKind = changeKind;
        }
    }
}