using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Echo.Xmpp.Parser;

namespace Echo.Xmpp.Connections
{
    public abstract class XmppConnection : IDisposable, IAsyncDisposable
    {
        protected readonly IXmppElementFactory ElementFactory;

        public abstract EndPoint? LocalEndpoint { get; }
        public abstract EndPoint? RemoteEndpoint { get; }
        public abstract bool IsConnecting { get; }
        public abstract bool IsConnected { get; }
        public abstract bool IsEncrypted { get; }

        public event EventHandler? StreamOpened;
        public event EventHandler? StreamClosed;
        public event EventHandler? Connected;
        public event EventHandler? Connecting;
        public event EventHandler? Disconnected;
        public event EventHandler<XmppConnectionErrorEventArgs>? ConnectionError;
        public event EventHandler<XmppConnectionErrorEventArgs>? ConnectionFailed;
        public event EventHandler<XmppConnectionCertificateValidationEventArgs, bool>? CertificateValidation;
        public event EventHandler<XmppConnectionCertificateValidationFailedEventArgs>? CertificateValidationFailed;
        public event EventHandler? CertificateFalidationSucceed;
        public event EventHandler<XmppConnectionXmlParsingFailedEventArgs>? XmlParsingFailed;
        public event EventHandler<XmppConnectionXmlElementEventArgs>? XmlElementSent;
        public event EventHandler<XmppConnectionXmlElementEventArgs>? XmlElementReceived;

        protected void OnStreamOpened()
        {
            StreamOpened?.Invoke(this, EventArgs.Empty);
        }

        protected void OnStreamClosed()
        {
            StreamClosed?.Invoke(this, EventArgs.Empty);
        }

        protected void OnConnected()
        {
            Connected?.Invoke(this, EventArgs.Empty);
        }

        protected void OnConnecting()
        {
            Connecting?.Invoke(this, EventArgs.Empty);
        }

        protected void OnDisconnected()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        protected void OnConnectionError(XmppConnectionErrorEventArgs e)
        {
            ConnectionError?.Invoke(this, e);
        }

        protected void OnConnectionFailed(XmppConnectionErrorEventArgs e)
        {
            ConnectionFailed?.Invoke(this, e);
        }

        protected void OnXmlParsingFailed(XmppConnectionXmlParsingFailedEventArgs e)
        {
            XmlParsingFailed?.Invoke(this, e);
        }

        protected void OnXmlElementSent(XmppConnectionXmlElementEventArgs e)
        {
            XmlElementSent?.Invoke(this, e);
        }

        protected void OnXmlElementReceived(XmppConnectionXmlElementEventArgs e)
        {
            XmlElementReceived?.Invoke(this, e);
        }

        protected void OnCertificateValidationSucceed()
        {
            CertificateFalidationSucceed?.Invoke(this, EventArgs.Empty);
        }

        protected void OnCertificateValidationFailed(XmppConnectionCertificateValidationFailedEventArgs e)
        {
            CertificateValidationFailed?.Invoke(this, e);
        }

        protected bool OnCertificateValidation(XmppConnectionCertificateValidationEventArgs e)
        {
            var certificateValidation = CertificateValidation;

            if (certificateValidation is not null)
            {
                return certificateValidation(this, e);
            }

            return true;
        }

        protected XmppConnection(IXmppElementFactory elementFactory)
        {
            ElementFactory = elementFactory ?? throw new ArgumentNullException(nameof(elementFactory));
        }

        public abstract void Dispose();
        public abstract ValueTask DisposeAsync();
        public abstract ValueTask<bool> OpenAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> CloseAsync(bool closeStream = true, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> OpenStreamAsync(CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> CloseStreamAsync(CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> SendAsync(XElement element, CancellationToken cancellationToken = default);

        protected XElement CreateElement(XmlReader reader)
        {
            string? namespaceUri = null;
            bool empty = false;
            XElement? element = null;
            XElement? currentElement = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                    {
                        empty = reader.IsEmptyElement;
                        namespaceUri = reader.NamespaceURI == string.Empty ? element!.Name.NamespaceName : reader.NamespaceURI;
                        currentElement = ElementFactory.Create(XNamespace.Get(namespaceUri).GetName(reader.LocalName));

                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            currentElement.Add(new XAttribute(XNamespace.Get(reader.Prefix.Length == 0 ? string.Empty : reader.NamespaceURI).GetName(reader.LocalName), reader.Value));
                        }

                        if (element is not null)
                        {
                            element.Add(currentElement);
                        }

                        if (!empty || empty && reader.Depth == 0)
                        {
                            element = currentElement;
                        }
                        else if (reader.Depth > 0)
                        {
                            element = currentElement.Parent ?? currentElement;
                        }

                        break;
                    }

                    case XmlNodeType.EndElement:
                    {
                        if (reader.Depth > 0)
                        {
                            element = element!.Parent;
                        }
                        break;
                    }

                    case XmlNodeType.Text:
                    {
                        if (element is not null)
                        {
                            element.Value = reader.Value;
                        }
                        break;
                    }
                }
            }

            return element!;
        }
    }
}