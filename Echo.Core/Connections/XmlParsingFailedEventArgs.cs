using System;
using System.Xml;

namespace Echo.Core.Connections
{
    public sealed class XmlParsingFailedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public XmlException Error { get; }

        public XmlParsingFailedEventArgs(IXmppConnection connection, XmlException error)
        {
            Connection = connection;
            Error = error;
        }
    }
}