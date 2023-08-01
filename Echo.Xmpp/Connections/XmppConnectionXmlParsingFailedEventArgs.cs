using System.Xml;

namespace Echo.Xmpp.Connections
{
    public readonly struct XmppConnectionXmlParsingFailedEventArgs
    {
        public XmlException Exception { get; }

        public XmppConnectionXmlParsingFailedEventArgs(XmlException exception)
        {
            Exception = exception;
        }
    }
}