using System;
using System.Xml;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppParsingFailedEventArgs : EventArgs
    {
        public XmlException Error
        {
            get;
            private set;
        }

        public XmppParsingFailedEventArgs(XmlException error)
        {
            Error = error;
        }
    }
}