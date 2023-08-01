using System.Xml;

namespace Echo.Xmpp.Parser
{
    public readonly struct XmppParsingFailedEventArgs
    {
        public XmlException Exception { get; }

        public XmppParsingFailedEventArgs(XmlException exception)
        {
            Exception = exception;
        }
    }
}