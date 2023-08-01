using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public readonly struct XmlElementSentEventArgs
    {
        public XmppConnectionService Connection { get; }
        public XElement Element { get; }

        public XmlElementSentEventArgs(XmppConnectionService connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}