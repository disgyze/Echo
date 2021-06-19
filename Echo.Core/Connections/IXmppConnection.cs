using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.Extensibility;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public interface IXmppConnection : ISupportsCommand
    {
        bool IsDisposed { get; }
        ConnectionEndPoint? LocalEndPoint { get; }
        ConnectionEndPoint? RemoteEndPoint { get; }
        ConnectionState State { get; }
        SecurityState SecurityState { get; }
        EncryptionProtocol EncryptionProtocol { get; }
        IAccount Account { get; }
        IWindow Window { get; }

        ValueTask<bool> OpenAsync();
        ValueTask<bool> CloseAsync();
        ValueTask<bool> SendAsync(XElement element);
        ValueTask<bool> SecureAsync();
        ValueTask<bool> OpenXmlStreamAsync(string? domain = null);
        ValueTask<bool> CloseXmlStreamAsync();
    }
}