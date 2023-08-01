using System.Xml.Linq;

namespace Echo.Xmpp.Connections
{
    public readonly struct XmppConnectionXmlElementEventArgs
    {
        public XElement Element { get; }

        public XmppConnectionXmlElementEventArgs(XElement element)
        {
            Element = element;
        }
    }
}