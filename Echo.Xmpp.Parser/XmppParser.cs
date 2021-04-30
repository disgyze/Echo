using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppParser : XmppParserBase
    {
        #region Nested Types

        enum XmppParserState
        {
            Initial,
            Tag,
            AttributeName,
            AttributeValue,
            PIOpen,
            PIClose,
            ElementOpen,
            ElementEmpty,
            ElementValue,
            ElementClose
        }

        #endregion

        #region Fields

        const int DefaultBufferSize = 32 * 1024;
        XmppParserState state = XmppParserState.Initial;
        XmlParserContext readerContext = null;
        XmlReaderSettings readerSettings = null;
        XmlNamespaceManager nsMgr = null;
        IXmppElementFactory elementFactory = null;
        StringBuilder buffer = null;
        bool canceled = false;

        #endregion

        #region Constructors

        public XmppParser(IXmppElementFactory elementFactory)
        {
            this.elementFactory = elementFactory;
            MaxBufferSize = DefaultBufferSize;
            buffer = new StringBuilder();
            nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace(string.Empty, "jabber:client");
            nsMgr.AddNamespace("stream", "http://etherx.jabber.org/streams");
            readerContext = new XmlParserContext(null, nsMgr, null, XmlSpace.None);
            readerSettings = new XmlReaderSettings();            
            readerSettings.XmlResolver = null;
            readerSettings.IgnoreWhitespace = false;
            readerSettings.IgnoreProcessingInstructions = true;
            readerSettings.CloseInput = true;
            readerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            readerSettings.ValidationType = ValidationType.None;
            readerSettings.DtdProcessing = DtdProcessing.Ignore;
        }

        #endregion

        #region Methods

        // TODO ???
        public override void Parse(ReadOnlySpan<byte> data)
        {
            Parse(data.ToArray());
        }

        public override void Parse(byte[] data)
        {
            if (canceled || data == null || data != null && data.Length == 0)
            {
                return;
            }

            foreach (char c in data)
            {
                if (canceled)
                {
                    return;
                }

                // TODO вместо добавления посимвольно, добавлять все сразу (перед ProcessBuffer)?
                buffer.Append(c);

                // TODO нужно складывать все полученные data.Length и их сравнивать с BufferSize
                if (buffer.Length > MaxBufferSize)
                {
                    OnBufferOverflowError();
                    Reset();
                    break;
                }

                switch (state)
                {
                    case XmppParserState.Initial:
                        if (c == '<')
                        {
                            state = XmppParserState.Tag;
                        }
                        break;

                    case XmppParserState.Tag:
                        switch (c)
                        {
                            case '?':
                                state = XmppParserState.PIOpen;
                                break;

                            case '/':
                                state = XmppParserState.ElementClose;
                                break;

                            default:
                                Depth++;
                                state = XmppParserState.ElementOpen;
                                break;
                        }
                        break;

                    case XmppParserState.ElementOpen:
                        if (c == '/')
                        {
                            state = XmppParserState.ElementEmpty;
                        }
                        else if (c == '>')
                        {
                            if (Depth == 1)
                            {
                                buffer.Length = 0;
                                OnStreamStart();
                                state = XmppParserState.Initial;
                                break;
                            }
                            state = XmppParserState.ElementValue;
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            state = XmppParserState.AttributeName;
                        }
                        break;

                    case XmppParserState.ElementEmpty:
                        if (c == '>')
                        {
                            Depth--;

                            if (Depth == 0)
                            {
                                buffer.Length = 0;
                                OnStreamEnd();
                            }
                            else if (Depth == 1)
                            {
                                ParseElement();
                            }

                            state = XmppParserState.ElementValue;
                        }
                        break;

                    case XmppParserState.ElementValue:
                        if (c == '<')
                        {
                            state = XmppParserState.Tag;
                        }
                        break;

                    case XmppParserState.ElementClose:
                        if (c == '>')
                        {
                            Depth--;

                            if (Depth == 0)
                            {
                                buffer.Length = 0;
                                OnStreamEnd();
                            }
                            else if (Depth == 1)
                            {
                                ParseElement();
                            }

                            state = XmppParserState.Initial;
                        }
                        break;

                    case XmppParserState.AttributeName:
                    {
                        if (c == '\'' || c == '"')
                        {
                            state = XmppParserState.AttributeValue;
                        }
                        else if (c == '/')
                        {
                            state = XmppParserState.ElementClose;
                        }
                        else if (c == '>')
                        {
                            state = XmppParserState.Initial;
                        }
                        break;
                    }

                    case XmppParserState.AttributeValue:
                    {
                        if (c == '\'' || c == '"')
                        {
                            state = XmppParserState.ElementOpen;
                        }
                        break;
                    }

                    case XmppParserState.PIOpen:
                        if (c == '?')
                        {
                            state = XmppParserState.PIClose;
                        }
                        break;

                    case XmppParserState.PIClose:
                        if (c == '>')
                        {
                            Depth = 0;
                            state = XmppParserState.Initial;
                        }
                        break;
                }
            }
            canceled = false;
        }

        private void ParseElement()
        {
            if (buffer.Length == 0)
            {
                return;
            }

            XmlReader reader = XmlReader.Create(new StringReader(buffer.ToString()), readerSettings, readerContext);
            try
            {
                XElement element = null;
                XElement currentElement = null;
                string nameSpace = null;
                bool empty = false;

                while (reader.Read())
                {
                    if (canceled)
                    {
                        return;
                    }

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        empty = reader.IsEmptyElement;
                        nameSpace = reader.NamespaceURI == string.Empty ? element.Name.NamespaceName : reader.NamespaceURI;
                        currentElement = elementFactory.Create(XName.Get(reader.LocalName, nameSpace));

                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            currentElement.Add(new XAttribute(reader.Name, reader.Value));
                        }

                        if (element != null)
                        {
                            element.Add(currentElement);
                        }

                        if (!empty || empty && reader.Depth == 0)
                        {
                            element = currentElement;
                        }
                        else if (reader.Depth > 0)
                        {
                            element = currentElement.Parent != null ? currentElement.Parent : currentElement;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Depth > 0)
                        {
                            element = element.Parent;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (element != null)
                        {
                            element.Value = reader.Value;
                        }
                    }
                }

                OnElementParsed(new XmppElementEventArgs(element));
            }
            catch (XmlException e)
            {
                OnParsingError(new XmppParsingFailedEventArgs(e));
            }
            finally
            {
                canceled = false;
                buffer.Length = 0;
                reader.Dispose();
            }
        }

        public override void Reset()
        {
            Depth = 0;
            canceled = false;
            state = XmppParserState.Initial;
            buffer.Length = 0;
        }

        public override void Cancel()
        {
            canceled = true;
        }

        #endregion
    }
}