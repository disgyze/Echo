using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Echo.Xmpp.ElementModel;

namespace Echo.Xmpp.Connections
{
    public sealed class XmppStanzaRequestFactory : IDisposable, IAsyncDisposable
    {
        int disposed = 0;
        XmppConnection connection;
        ConcurrentDictionary<string, TaskCompletionSource<XmppStanza?>> responseCollection = new ConcurrentDictionary<string, TaskCompletionSource<XmppStanza?>>();

        /// <summary>
        /// Default 30 seconds timeout used for requests.
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        public XmppStanzaRequestFactory(XmppConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.connection.Disconnected += EventDisconnected;
            this.connection.XmlElementReceived += EventXmlElementReceived;
        }

        public void Dispose()
        {
            var task = DisposeAsync();

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0)
            {
                return ValueTask.CompletedTask;
            }

            connection.Disconnected -= EventDisconnected;
            connection.XmlElementReceived -= EventXmlElementReceived;
            Reset();

            return ValueTask.CompletedTask;
        }

        public async Task<XmppStanza?> CreateAsync(XmppStanza stanza, TimeSpan timeout = default, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (stanza is null)
            {
                throw new ArgumentNullException(nameof(stanza));
            }

            if (string.IsNullOrWhiteSpace(stanza.Id))
            {
                throw new ArgumentException($"{nameof(XmppStanza.Id)} cannot be null or empty", nameof(stanza));
            }

            var response = new TaskCompletionSource<XmppStanza?>(TaskCreationOptions.RunContinuationsAsynchronously);

            if (!responseCollection.TryAdd(stanza.Id, response))
            {
                throw new InvalidOperationException("Request with the specified ID is already exist.");
            }

            try
            {
                await connection.SendAsync(stanza, cancellationToken).ConfigureAwait(false);
                return await response.Task.WaitAsync(timeout == default ? DefaultTimeout : timeout, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                responseCollection.TryRemove(stanza.Id, out var _);
                throw;
            }
        }

        private void EventDisconnected(object? sender, EventArgs e)
        {
            Reset();
        }

        private void EventXmlElementReceived(object? sender, XmppConnectionXmlElementEventArgs e)
        {
            if (e.Element is XmppStanza responseStanza && responseStanza.Id is not null)
            {
                if (responseCollection.TryRemove(responseStanza.Id, out var response))
                {
                    response.SetResult(responseStanza);
                }
            }
        }

        private void Reset()
        {
            foreach (var (_, completionSource) in responseCollection)
            {
                completionSource.SetCanceled();
            }
            responseCollection.Clear();
        }

        private void ThrowIfDisposed()
        {
            if (disposed == 1)
            {
                throw new ObjectDisposedException(nameof(XmppStanzaRequestFactory));
            }
        }
    }
}