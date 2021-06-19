using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public interface IXmppClient
    {
        Guid Id { get; }
        ConnectionEndPoint? LocalEndPoint { get; }
        ConnectionEndPoint? RemoteEndPoint { get; }
        SecurityState SecurityState { get; }
        ConnectionState ConnectionState { get; }
        EncryptionProtocol EncryptionProtocol { get; }
        IAccount Account { get; }
        IWindow Window { get; }

        ValueTask<bool> ConnectAsync(bool rejoinChannels = true, CancellationToken cancellationToken = default);
        ValueTask<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default);
        ValueTask<bool> SendAsync(XElement? element, CancellationToken cancellationToken = default);
        ValueTask<bool> StartStreamAsync(string? domain = null, CancellationToken cancellationToken = default);
        ValueTask<bool> CloseStreamAsync(CancellationToken cancellationToken = default);
    }
}