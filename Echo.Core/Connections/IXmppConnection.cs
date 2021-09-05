using System;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public interface IXmppConnection : IServiceProvider, ISupportsCommand
    {
        ConnectionEndPoint? LocalEndPoint { get; }
        ConnectionEndPoint? RemoteEndPoint { get; }
        ConnectionState ConnectionState { get; }
        SecurityState SecurityState { get; }
        EncryptionProtocol EncryptionProtocol { get; }
        IAccount Account { get; }
        IWindow Window { get; }
        IXmppStream Stream { get; }

        Task<bool> OpenAsync(bool forceReconnect = false);
        Task<bool> CloseAsync();
    }
}