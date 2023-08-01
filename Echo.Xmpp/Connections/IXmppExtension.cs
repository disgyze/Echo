using System;
using System.Threading.Tasks;

namespace Echo.Xmpp.Connections
{
    public interface IXmppExtension : IDisposable, IAsyncDisposable
    {
        ValueTask InitializeAsync(XmppConnection connetion);
    }
}