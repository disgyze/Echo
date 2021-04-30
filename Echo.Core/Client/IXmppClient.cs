using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Client
{
    public interface IXmppClient
    {
        Guid Id { get; }
        ConnectionEndpoint LocalEndpoint { get; }
        ConnectionEndpoint RemoteEndpoint { get; }
        EncryptionProtocol Encryption { get; }
        IAccount Account { get; }
        IWindow Window { get; }
        bool IsConnecting { get; }
        bool IsConnected { get; }
        bool IsEncrypting { get; }
        bool IsEncrypted { get; }

        Task<bool> ConnectAsync(bool rejoinChannels = true, CancellationToken cancellationToken = default);
        Task<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default);
        Task<bool> SendAsync(XElement element, CancellationToken cancellationToken = default);
        Task<bool> StartStreamAsync(string? domain = null, CancellationToken cancellationToken = default);
        Task<bool> CloseStreamAsync(CancellationToken cancellationToken = default);
    }
}