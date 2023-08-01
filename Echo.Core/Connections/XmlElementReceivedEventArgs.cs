using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public readonly struct XmlElementReceivedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public XElement Element { get; }

        public XmlElementReceivedEventArgs(XmppConnectionService connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}