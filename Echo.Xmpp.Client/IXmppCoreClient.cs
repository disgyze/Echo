using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Xmpp.Parser;

namespace Echo.Xmpp.Client
{
    public interface IXmppCoreClient : IDisposable, IAsyncDisposable
	{
		//event EventHandler StreamStarted;
		//event EventHandler StreamClosed;
		//event EventHandler Connecting;
		//event EventHandler Connected;
		//event EventHandler ConnectionFailed;
		//event EventHandler Disconnected;
		//event EventHandler DnsResolving;
		//event EventHandler DnsResolved;
		//event EventHandler DnsResolutionFailed;
		//event EventHandler CertificateValidating;
		//event EventHandler CertificateValidated;
		//event EventHandler CertificateValidationFailed;
		//event EventHandler XmlParsingFailed;
		event EventHandler<XmppElementEventArgs<XElement>> XmlElementReceived;
		event EventHandler<XmppElementEventArgs<XElement>> XmlElementSent;

		bool IsConnected { get; }
		bool IsConnecting { get; }
		bool IsEncrypted { get; }
		bool IsEncrypting { get; }

		ValueTask<bool> SendAsync(XElement element, CancellationToken cancellationToken = default);
		ValueTask<bool> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
		ValueTask<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default);
		ValueTask<bool> StartStreamAsync(string? domain = null, CancellationToken cancellationToken = default);
		ValueTask<bool> CloseStreamAsync(CancellationToken cancellationToken = default);
	}
}