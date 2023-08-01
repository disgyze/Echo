using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public abstract class ContactStore
    {
        public abstract ValueTask<bool> SaveContactAsync(Contact contact, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UpdateContactAsync(Contact contact, ContactChangeKind changeKind, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteContactAsync(Contact contact, CancellationToken cancellationToken = default);
        public abstract ValueTask<Contact?> GetContactAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<Contact> GetContactsAsync();
    }
}