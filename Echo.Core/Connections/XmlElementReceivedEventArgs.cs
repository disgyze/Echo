using System;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public sealed class XmlElementReceivedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public XElement Element { get; }

        public XmlElementReceivedEventArgs(IXmppConnection connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}