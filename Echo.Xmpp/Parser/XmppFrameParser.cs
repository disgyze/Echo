using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppFrameParser : XmppParser
    {
        public XmppFrameParser(IXmppElementFactory elementFactory) : base(elementFactory)
        {
        }

        public override void Parse(ReadOnlySpan<byte> data)
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

        public override void Reset()
        {           
        }
    }
}