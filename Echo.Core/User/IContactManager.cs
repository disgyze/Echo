using System;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public interface IContactManager
    {
        int Count { get; }
        string Version { get; }
        bool IsSyncing { get; }

        IContact? GetContact(int contactIndex);
        IContact? GetContact(Guid contactId);
        IContact? GetContact(XmppAddress contactAddress);
        ValueTask<bool> SyncAsync();
    }
}