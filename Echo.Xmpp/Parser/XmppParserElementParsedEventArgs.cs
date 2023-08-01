using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public readonly struct XmppParserElementParsedEventArgs
    {
        public XElement Element { get; }

        public XmppParserElementParsedEventArgs(XElement element)
        {
            Element = element;
        }
    }
}