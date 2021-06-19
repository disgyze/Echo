using System;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public sealed class XmlElementSentEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public XElement Element { get; }

        public XmlElementSentEventArgs(IXmppClient connection, XElement element)
        {
            Connection = connection;
            Element = element;
        }
    }
}