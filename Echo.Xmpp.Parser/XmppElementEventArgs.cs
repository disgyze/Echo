using System;
using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppElementEventArgs : EventArgs
    {
        public XElement Element
        {
            get;
            private set;
        }

        public XmppElementEventArgs(XElement element)
        {
            Element = element;
        }
    }

    public sealed class XmppElementEventArgs<TElement> : EventArgs where TElement : XElement
    {
        public TElement Element
        {
            get;
            private set;
        }

        public XmppElementEventArgs(TElement element)
        {
            Element = element;
        }
    }
}