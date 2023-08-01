using System.Collections.Generic;

namespace Echo.Core.User
{
    public abstract class ContactStoreManager
    {
        public abstract ContactStore? GetContactStore(Account account);
        public abstract ContactStore? GetContactStore(XmppAddress accountAddress);
        public abstract IAsyncEnumerable<Contact> GetContactsAsync();
    }
}