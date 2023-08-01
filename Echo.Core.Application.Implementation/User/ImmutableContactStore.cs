using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;

namespace Echo.Core.User
{
    //public sealed class ImmutableContactStore : ContactStore
    //{
    //    static class ErrorMessage
    //    {
    //        public static readonly string ContactFromDifferentAccountNotAccepted = "Contact cannot belong to a different account";
    //    }

    //    XmppAddress accountAddress;
    //    ImmutableList<Contact> contactCollection;
    //    Event<ContactAddedEventArgs> contactAdded;
    //    Event<ContactRemovedEventArgs> contactRemoved;
    //    Event<ContactChangedEventArgs> contactChanged;

    //    public ImmutableContactStore(XmppAddress accountAddress, EventChannel eventChannel, ImmutableList<Contact>? contactCollection = null)
    //    {
    //        if (accountAddress is null)
    //        {
    //            throw new ArgumentNullException(nameof(accountAddress));
    //        }

    //        if (eventChannel is null)
    //        {
    //            throw new ArgumentNullException(nameof(eventChannel));
    //        }

    //        this.accountAddress = accountAddress;
    //        this.contactCollection = contactCollection ?? ImmutableList<Contact>.Empty;

    //        contactAdded = eventChannel.GetEvent<ContactAddedEventArgs>();
    //        contactRemoved = eventChannel.GetEvent<ContactRemovedEventArgs>();
    //        contactChanged = eventChannel.GetEvent<ContactChangedEventArgs>();
    //    }

    //    public override ValueTask<bool> SaveContactAsync(Contact contact, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (contact is null)
    //        {
    //            throw new ArgumentNullException(nameof(contact));
    //        }

    //        if (!contact.AccountAddress.EqualsBare(accountAddress))
    //        {
    //            throw new ArgumentException(ErrorMessage.ContactFromDifferentAccountNotAccepted, nameof(contact));
    //        }

    //        if (ImmutableInterlocked.Update(ref contactCollection, Save, contact))
    //        {
    //            _ = contactAdded.PublishAsync(new ContactAddedEventArgs(contact));
    //            return ValueTask.FromResult(true);
    //        }

    //        return ValueTask.FromResult(false);

    //        static ImmutableList<Contact> Save(ImmutableList<Contact> collection, Contact contact) =>
    //            collection.FirstOrDefault(otherContact => otherContact.Address.EqualsBare(contact.Address)) is null ? collection.Add(contact) : collection;
    //    }

    //    public override ValueTask<bool> UpdateContactAsync(Contact contact, ContactChangeKind changeKind, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (contact is null)
    //        {
    //            throw new ArgumentNullException(nameof(contact));
    //        }

    //        if (!contact.AccountAddress.EqualsBare(accountAddress))
    //        {
    //            throw new ArgumentException(ErrorMessage.ContactFromDifferentAccountNotAccepted, nameof(contact));
    //        }

    //        Contact? oldContact = contactCollection.FirstOrDefault(otherContact => otherContact.Address.EqualsBare(contact.Address));

    //        if (oldContact is null)
    //        {
    //            return ValueTask.FromResult(false);
    //        }

    //        try
    //        {
    //            ImmutableInterlocked.Update(ref contactCollection, (collection, contact) => collection.Replace(oldContact, contact), contact);
    //            _ = contactChanged.PublishAsync(new ContactChangedEventArgs(contact, oldContact, changeKind));
    //        }
    //        catch (ArgumentException)
    //        {
    //            return ValueTask.FromResult(false);
    //        }

    //        return ValueTask.FromResult(true);
    //    }

    //    public override ValueTask<bool> DeleteContactAsync(Contact contact, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (contact is null)
    //        {
    //            throw new ArgumentNullException(nameof(contact));
    //        }

    //        if (!contact.AccountAddress.EqualsBare(accountAddress))
    //        {
    //            throw new ArgumentException(ErrorMessage.ContactFromDifferentAccountNotAccepted, nameof(contact));
    //        }

    //        if (ImmutableInterlocked.Update(ref contactCollection, Delete, contact))
    //        {
    //            _ = contactRemoved.PublishAsync(new ContactRemovedEventArgs(contact));
    //            return ValueTask.FromResult(true);
    //        }

    //        return ValueTask.FromResult(false);

    //        static ImmutableList<Contact> Delete(ImmutableList<Contact> collection, Contact contact) =>
    //            collection.FirstOrDefault(otherContact => otherContact.Address.EqualsBare(contact.Address)) is Contact otherContact ? collection.Remove(otherContact) : collection;
    //    }

    //    public override ValueTask<Contact?> GetContactAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (address is null)
    //        {
    //            throw new ArgumentNullException(nameof(address));
    //        }

    //        return ValueTask.FromResult(contactCollection.FirstOrDefault(contact => contact.Address.EqualsBare(address)));
    //    }

    //    public override async IAsyncEnumerable<Contact> GetContactsAsync()
    //    {
    //        foreach (var contact in contactCollection)
    //        {
    //            yield return contact;
    //        }
    //        await Task.CompletedTask.ConfigureAwait(false);
    //    }
    //}
}