using System;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public sealed class XmlElementReceivedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public XElement Element { get; }

        public XmlElementReceivedEventArgs(IXmppClient connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}