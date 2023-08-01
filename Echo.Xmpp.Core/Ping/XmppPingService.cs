using Echo.Xmpp.Connections;
using Echo.Xmpp.ElementModel;

namespace Echo.Xmpp.Core.Ping
{
    public sealed class XmppPingService : IXmppPingService
    {
        bool disposed = false;
        XmppConnection connection;
        XmppPingServiceAutoReply autoReply;

        public XmppPingService(XmppConnection client, XmppPingServiceAutoReply autoReply = XmppPingServiceAutoReply.Disabled)
        {
            this.connection = client ?? throw new ArgumentNullException(nameof(client));
            this.autoReply = autoReply;

            if (autoReply != XmppPingServiceAutoReply.Disabled)
            {
                client.XmlElementReceived += EventElementReceived;
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            Reset();
            disposed = true;
        }

        public ValueTask<bool> PingAsync(string sender, string target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sender))
            {
                throw new ArgumentException(null, nameof(sender));
            }

            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException(null, nameof(target));
            }

            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
            //return connection.SendAsync(new XmppIQ(XmppIQKind.Get, XmppIQ.GenerateId(), sender, target, new XmppPing()), cancellationToken);
        }

        public ValueTask<bool> PongAsync(string id, string sender, string target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(null, nameof(id));
            }

            if (string.IsNullOrWhiteSpace(sender))
            {
                throw new ArgumentException(null, nameof(sender));
            }

            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException(null, nameof(target));
            }

            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
            //return connection.SendAsync(new XmppIQ(XmppIQKind.Result, id, sender, target), cancellationToken);
        }

        private void EventElementReceived(object? sender, XmppConnectionXmlElementEventArgs e)
        {
            if (e.Element is XmppIQ iq && iq.Kind == XmppIQKind.Get && iq.Element(XmppPing.ElementName) is not null)
            {
                if (string.IsNullOrWhiteSpace(iq.Id) || string.IsNullOrWhiteSpace(iq.Target) || string.IsNullOrWhiteSpace(iq.Sender))
                {
                    return;
                }

                switch (autoReply)
                {
                    case XmppPingServiceAutoReply.Pong:
                    {
                        try
                        {
                            _ = PongAsync(iq.Id, iq.Target, iq.Sender);
                        }
                        catch (ObjectDisposedException)
                        {
                            Reset();
                        }
                        break;
                    }

                    case XmppPingServiceAutoReply.Error:
                    {
                        throw new NotImplementedException();
                    }

                    default: throw new NotImplementedException();
                }
            }
        }

        private void Reset()
        {
            connection.XmlElementReceived -= EventElementReceived;
        }
    }
}