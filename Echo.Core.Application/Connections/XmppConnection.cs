using System;
using System.Threading.Tasks;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public abstract class XmppConnection : IXmppConnection
    {
        public abstract ConnectionEndPoint LocalEndPoint { get; }
        public abstract ConnectionEndPoint RemoteEndPoint { get; }
        public abstract ConnectionState ConnectionState { get; }
        public abstract SecurityState SecurityState { get; }
        public abstract EncryptionProtocol EncryptionProtocol { get; }
        public abstract IAccount Account { get; }
        public abstract IWindow Window { get; }
        public abstract IXmppStream Stream { get; }

        public abstract object GetService(Type serviceType);
        public abstract ValueTask CommandAsync(string text);
        public abstract Task<bool> CloseAsync();
        public abstract Task<bool> OpenAsync(bool forceReconnect = false);
    }
}