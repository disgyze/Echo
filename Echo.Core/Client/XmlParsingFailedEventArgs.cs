using System;
using System.Xml;

namespace Echo.Core.Client
{
    public sealed class XmlParsingFailedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public XmlException Error { get; }

        public XmlParsingFailedEventArgs(IXmppClient connection, XmlException error)
        {
            Connection = connection;
            Error = error;
        }
    }
}