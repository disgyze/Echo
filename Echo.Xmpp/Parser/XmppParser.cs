using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Echo.Xmpp.Parser
{
    public abstract class XmppParser : IXmppParser
    {
        XmlNamespaceManager namespaceManager;
        XmlReaderSettings settings;
        XmlParserContext context;

        public event XmppParserEventHandler<XmppParserDeclarationParsedEventArgs>? DeclarationParsed;
        public event XmppParserEventHandler<XmppParserElementParsedEventArgs>? ElementParsed;
        public event XmppParserEventHandler<XmppParsingFailedEventArgs>? ParsingFailed;
        public event XmppParserEventHandler? StreamClosed;
        public event XmppParserEventHandler? StreamOpened;

        public IXmppElementFactory ElementFactory { get; }

        protected XmppParser(IXmppElementFactory elementFactory)
        {
            ElementFactory = elementFactory ?? throw new ArgumentNullException(nameof(elementFactory));
            namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace(string.Empty, "jabber:client");
            namespaceManager.AddNamespace("stream", "http://etherx.jabber.org/streams");
            context = new XmlParserContext(null, namespaceManager, null, XmlSpace.None, Encoding.UTF8);
            settings = new XmlReaderSettings()
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                IgnoreProcessingInstructions = true,
                CloseInput = true
            };
        }

        protected void OnDeclarationParsed(XmppParserDeclarationParsedEventArgs e)
        {
            DeclarationParsed?.Invoke(this, e);
        }

        protected void OnElementParsed(XmppParserElementParsedEventArgs e)
        {
            ElementParsed?.Invoke(this, e);
        }

        protected void OnParsingFailed(XmppParsingFailedEventArgs e)
        {
            ParsingFailed?.Invoke(this, e);
        }

        protected void OnStreamOpened()
        {
            StreamOpened?.Invoke(this);
        }

        protected void OnStreamClosed()
        {
            StreamClosed?.Invoke(this);
        }

        protected virtual XmlReader CreateReader(TextReader input)
        {
            return XmlReader.Create(input, settings, context);
        }

        public abstract void Parse(ReadOnlySpan<byte> data);
        public abstract void Reset();
    }
}