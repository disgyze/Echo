using System;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public sealed class XmlElementSentEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public XElement Element { get; }

        public XmlElementSentEventArgs(IXmppConnection connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}