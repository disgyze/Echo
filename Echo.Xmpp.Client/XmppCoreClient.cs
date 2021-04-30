using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Networking;
using Echo.Xmpp.ElementModel;
using Echo.Xmpp.Parser;

namespace Echo.Xmpp.Client
{
	public delegate void XmppStanzaCallback(XmppStanza stanza);

	public class XmppCoreClient
	{
		public const int DefaultPort = 5222;

		bool disposed = false;
		const string streamFormat =
			@"<?xml version='1.0' encoding='utf-8'?>
				<stream:stream
					to='{0}'
					version='1.0'
					xml:lang='en'
					xmlns='jabber:client'
					xmlns:stream='http://etherx.jabber.org/streams'>";
		const string streamClose = "</stream:stream>";

		public string Host
		{
			get;
			protected set;
		}

		public int Port
		{
			get;
			protected set;
		}

		public IXmppParser Parser
		{
			get;
			protected set;
		}

		public ClientSocket Socket
		{
			get;
			protected set;
		}

		public bool IsConnected => Socket?.State == ClientSocketState.Connected;
		public bool IsConnecting => Socket?.State == ClientSocketState.Connecting;
		public bool IsEncrypted => Socket != null && Socket.IsEncrypted;
		public bool IsEncrypting => Socket != null && Socket.IsEncrypting;
		public bool IsAuthenticated { get; private set; }

		public event EventHandler<XmppElementEventArgs<XElement>> XmlElementReceived;
		public event EventHandler<XmppElementEventArgs<XElement>> XmlElementSent;

		protected void OnElementReceived(XmppElementEventArgs<XElement> e)
		{
            XmlElementReceived?.Invoke(this, e);
        }

		protected void OnElementSent(XmppElementEventArgs<XElement> e)
		{
            XmlElementSent?.Invoke(this, e);
        }
 
		public XmppCoreClient(ClientSocket socket, IXmppParser parser)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}

			if (parser == null)
			{
				throw new ArgumentNullException("parser");
			}

			//DnsEndPoint endPoint = socket.RemoteEndPoint as DnsEndPoint;
			//Host = endPoint.Host;
			//Port = endPoint.Port;
			Parser = parser;
			Parser.ElementParsed += (s, e) => OnElementReceived(new XmppElementEventArgs<XElement>(e.Element));
			Socket = socket;
			Socket.DataReceived += (s, e) => parser.Parse(e.Data);
		}

		~XmppCoreClient()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (Socket != null)
					{
						Socket.DisconnectAsync().GetAwaiter().GetResult();
						Socket = null;
					}

					if (Parser != null)
					{
						Parser.Cancel();
						Parser = null;
					}
				}
				disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public ValueTask DisposeAsync()
		{
			Dispose(true);
			return default;
		}

		public Task<bool> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
		{
			return Socket.ConnectAsync(endpoint, cancellationToken: cancellationToken);
		}

		public async Task<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default)
		{
			if (closeStream)
            {
				await CloseStreamAsync(cancellationToken);
            }

			return await Socket.DisconnectAsync(cancellationToken);
		}

		public async Task<bool> SendAsync(XElement element, CancellationToken cancellationToken = default)
		{
			var sent = await Socket.SendAsync(Encoding.UTF8.GetBytes(element.ToString()), cancellationToken);

			if (sent)
			{
				OnElementSent(new XmppElementEventArgs<XElement>(element));
			}

			return sent;
		}

		public Task<bool> StartStreamAsync(string domain, CancellationToken cancellationToken = default)
		{
			return Socket.SendAsync(Encoding.UTF8.GetBytes(string.Format(streamFormat, domain)), cancellationToken);
		}

		public Task<bool> CloseStreamAsync(CancellationToken cancellationToken = default)
        {
			return Socket.SendAsync(Encoding.UTF8.GetBytes(streamClose), cancellationToken);
        }

		public Task<bool> EncryptAsync(SslProtocols sslProtocols, CancellationToken cancellationToken = default)
        {
			return Socket.UpgradeToSslAsync(sslProtocols, cancellationToken);
        }
    }
}