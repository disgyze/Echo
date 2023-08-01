using System.Xml;

namespace Echo.Core.Connections
{
    public readonly struct XmlParsingFailedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public XmlException Error { get; }

        public XmlParsingFailedEventArgs(XmppConnectionService connection, XmlException error)
        {
            Connection = connection;
            Error = error;
        }
    }
}