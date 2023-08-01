using System;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppStreamParser : XmppParser
    {
        enum XmppParserState
        {
            Initial,
            BracketOpen,
            ElementOpen,
            ElementClose,
            ElementEmpty,
            Attribute,
            DeclarationOpen,
            DeclarationClose,
        }

        enum XmppNodeKind
        {
            Element,
            Declaration,
            StreamOpen,
            StreamClose
        }

        const int StreamDepth = 1;
        const int ElementDepth = 2;

        int depth = 0;
        XmppParserState state = XmppParserState.Initial;
        Buffer<byte> buffer = new Buffer<byte>();

        public XmppStreamParser(IXmppElementFactory elementFactory) : base(elementFactory)
        {
        }

        public override void Parse(ReadOnlySpan<byte> data)
        {
            if (data.Length == 0)
            {
                return;
            }

            buffer.Write(data);

            var count = buffer.WrittenMemory.Length;
            var found = false;
            var i = 0;

            while (i < count)
            {
                char c = (char)buffer.WrittenMemory.Span[i];

                switch (state)
                {
                    case XmppParserState.Initial:
                    {
                        if (c == '<')
                        {
                            state = XmppParserState.BracketOpen;
                        }
                        break;
                    }

                    case XmppParserState.BracketOpen:
                    {
                        switch (c)
                        {
                            case '?':
                            {
                                state = XmppParserState.DeclarationOpen;
                                break;
                            }

                            case '/':
                            {
                                state = XmppParserState.ElementClose;
                                break;
                            }

                            default:
                            {
                                depth++;
                                state = XmppParserState.ElementOpen;
                                break;
                            }
                        }
                        break;
                    }

                    case XmppParserState.ElementOpen:
                    {
                        switch (c)
                        {
                            case '"' or '\'':
                            {
                                state = XmppParserState.Attribute;
                                break;
                            }

                            case '/':
                            {
                                state = XmppParserState.ElementEmpty;
                                break;
                            }

                            case '>':
                            {
                                if (depth == StreamDepth)
                                {
                                    OnNodeFound(i, XmppNodeKind.StreamOpen);
                                    found = true;
                                }
                                state = XmppParserState.Initial;
                                break;
                            }
                        }
                        break;
                    }

                    case XmppParserState.ElementClose:
                    {
                        if (c == '>')
                        {
                            depth--;

                            switch (depth)
                            {
                                case StreamDepth:
                                {
                                    OnNodeFound(i, XmppNodeKind.StreamClose);
                                    found = true;
                                    break;
                                }

                                case ElementDepth:
                                {
                                    OnNodeFound(i, XmppNodeKind.Element);
                                    found = true;
                                    break;
                                }
                            }

                            state = XmppParserState.Initial;
                        }
                        break;
                    }

                    case XmppParserState.ElementEmpty:
                    {
                        if (c == '>')
                        {
                            depth--;

                            if (depth == ElementDepth)
                            {
                                OnNodeFound(i, XmppNodeKind.Element);
                                found = true;
                            }

                            state = XmppParserState.Initial;
                        }
                        break;
                    }

                    case XmppParserState.Attribute:
                    {
                        if (c == '"' || c == '\'')
                        {
                            state = XmppParserState.ElementOpen;
                        }
                        break;
                    }

                    case XmppParserState.DeclarationOpen:
                    {
                        if (c == '?')
                        {
                            state = XmppParserState.DeclarationClose;
                        }
                        break;
                    }

                    case XmppParserState.DeclarationClose:
                    {
                        if (c == '>')
                        {
                            OnNodeFound(i, XmppNodeKind.Declaration);
                            found = true;
                            state = XmppParserState.Initial;
                        }
                        break;
                    }
                }

                if (found)
                {
                    i = 0;
                    count = buffer.WrittenMemory.Length;
                    found = false;
                    continue;
                }

                i++;
            }
        }

        private void OnNodeFound(int index, XmppNodeKind nodeKind)
        {
            var processedSpan = buffer.WrittenMemory.Span.Slice(0, index + 1);
            Debug.WriteLine(Encoding.UTF8.GetString(processedSpan));

            switch (nodeKind)
            {
                case XmppNodeKind.StreamOpen: OnStreamOpened(); break;
                case XmppNodeKind.StreamClose: OnStreamClosed(); break;
                case XmppNodeKind.Element: OnElementFound(ref processedSpan); break;
                case XmppNodeKind.Declaration: OnDeclarationFound(ref processedSpan); break;
            }

            var remainingLength = buffer.WrittenMemory.Length - index - 1;

            if (remainingLength > 0)
            {
                var remainingBuffer = new Buffer<byte>(buffer.WrittenMemory.Span.Slice(index + 1, remainingLength));
                buffer.Dispose();
                buffer = remainingBuffer;
                return;
            }

            buffer.Dispose();
            buffer = new Buffer<byte>();
        }

        private void OnElementFound(ref ReadOnlySpan<byte> data)
        {
            string? namespaceUri = null;
            bool empty = false;
            XElement? element = null;
            XElement? currentElement = null;
            XmlReader? reader = null;
            try
            {
                reader = CreateReader(ReusableTextReader.Shared.WithString(Encoding.UTF8.GetString(data)));

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                        {
                            empty = reader.IsEmptyElement;
                            namespaceUri = reader.NamespaceURI == string.Empty ? element!.Name.NamespaceName : reader.NamespaceURI;
                            currentElement = ElementFactory.Create(XName.Get(reader.LocalName, namespaceUri));

                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                currentElement.Add(new XAttribute(reader.Name, reader.Value));
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

                OnElementParsed(new XmppParserElementParsedEventArgs(element!));
            }
            catch (XmlException e)
            {
                OnParsingFailed(new XmppParsingFailedEventArgs(e));
            }
            finally
            {
                reader?.Dispose();
            }
        }

        private void OnDeclarationFound(ref ReadOnlySpan<byte> data)
        {
            XmlReader? reader = null;
            try
            {
                reader = CreateReader(ReusableTextReader.Shared.WithString(Encoding.UTF8.GetString(data)));
                reader.Read();

                XDeclaration declaration =
                    new XDeclaration(
                        reader.GetAttribute("version"),
                        reader.GetAttribute("encoding"),
                        reader.GetAttribute("standalone"));

                OnDeclarationParsed(new XmppParserDeclarationParsedEventArgs(declaration));
            }
            catch (XmlException e)
            {
                OnParsingFailed(new XmppParsingFailedEventArgs(e));
            }
            finally
            {
                reader?.Dispose();
            }
        }

        public override void Reset()
        {
            state = XmppParserState.Initial;
            depth = 0;
            buffer.Dispose();
            buffer = new Buffer<byte>();
        }
    }
}