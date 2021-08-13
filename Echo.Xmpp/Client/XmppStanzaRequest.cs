using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Xmpp.Parser;
using Echo.Xmpp.ElementModel;
using System.Xml.Linq;

namespace Echo.Xmpp.Client
{
    // TODO поменять XmppCoreClient на интерфейс
    public sealed class XmppStanzaRequest<TStanza> : IDisposable, IAsyncDisposable where TStanza : XmppStanza
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        bool disposed = false;
        TStanza? request = null;
        XmppCoreClient? client = null;
        CancellationTokenSource? cancellationSource = null;
        TaskCompletionSource<TStanza?>? completionSource = null;

        public XmppStanzaRequest(XmppCoreClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        ~XmppStanzaRequest()
        {
            Dispose(false);
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            ThrowIfDisposed();

            if (disposing)
            {
                CleanUp();
            }

            disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(XmppStanzaRequest<TStanza>));
            }
        }

        private void Cancel()
        {
            completionSource?.TrySetCanceled();
            CleanUp();
        }

        private void CleanUp()
        {
            if (cancellationSource != null)
            {
                cancellationSource.Dispose();
                cancellationSource = null;
            }

            if (client != null)
            {
                client.XmlElementReceived -= EventElementReceived;
                client = null;
            }
        }

        public ValueTask<TStanza?> GetResponseAsync(TStanza stanza, TimeSpan timeout = default, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<TStanza?>(cancellationToken);
                //return Task.FromResult<TStanza?>(null);
            }

            if (!client!.IsConnected)
            {
                return ValueTask.FromResult<TStanza?>(null);
                //return Task.FromResult<TStanza?>(null);
            }

            request = stanza ?? throw new ArgumentNullException(nameof(stanza));

            completionSource = new TaskCompletionSource<TStanza?>();
            cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cancellationSource.CancelAfter(timeout != default ? timeout : DefaultTimeout);
            cancellationSource.Token.Register(Cancel, useSynchronizationContext: false);

            client!.XmlElementReceived += EventElementReceived;
            _ = client!.SendAsync(stanza, cancellationToken);

            return new ValueTask<TStanza?>(completionSource.Task);
        }

        private void EventElementReceived(object? sender, XmppElementEventArgs<XElement> e)
        {
            if (cancellationSource != null && cancellationSource.IsCancellationRequested)
            {
                Cancel();
                return;
            }

            if (e.Element is XmppStanza response)
            {
                if (request!.Id == response.Id)
                {
                    completionSource?.TrySetResult((TStanza)response);
                }
            }
        }
    }
}