using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections
{
    //public sealed class XmppStanzaRequest<TStanza> : IDisposable where TStanza : XmppStanza
    //{
    //    bool disposed = false;
    //    string? requestStanzaId = null;
    //    XmppConnection? connection = null;
    //    IDisposable? xmlElementReceivedToken = null;
    //    TaskCompletionSource<TStanza>? completionSource = null;

    //    public XmppStanzaRequest(XmppConnection connection, EventService eventService)
    //    {
    //        this.connection = connection;
    //        xmlElementReceivedToken = eventService.RegisterEvent<XmppElementReceivedEventArgs>(EventXmlElementReceived);
    //    }

    //    public void Dispose()
    //    {
    //        if (disposed)
    //        {
    //            return;
    //        }

    //        xmlElementReceivedToken?.Dispose();
    //        xmlElementReceivedToken = null;

    //        disposed = true;
    //    }

    //    public async ValueTask<TStanza?> GetResponseAsync(TStanza stanza, CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (stanza is null)
    //        {
    //            throw new ArgumentNullException(nameof(stanza));
    //        }

    //        if (connection is not null && connection.State == ConnectionState.Opened)
    //        {
    //            requestStanzaId = stanza.Id;

    //            if (!await connection.SendAsync(stanza, cancellationToken))
    //            {
    //                return null;
    //            }
    //        }

    //        completionSource = new TaskCompletionSource<TStanza>();

    //        return await completionSource.Task;
    //    }

    //    private ValueTask<EventResult> EventXmlElementReceived(XmppElementReceivedEventArgs e)
    //    {
    //        if (e.Element is TStanza responseStanza && completionSource is not null && string.Equals(requestStanzaId, responseStanza.Id, StringComparison.OrdinalIgnoreCase))
    //        {
    //            completionSource.SetResult(responseStanza);
    //        }
    //        return ValueTask.FromResult(EventResult.Continue);
    //    }

    //    private void ThrowIfDisposed()
    //    {
    //        if (disposed)
    //        {
    //            throw new ObjectDisposedException(nameof(XmppStanzaRequest<TStanza>));
    //        }
    //    }
    //}
}