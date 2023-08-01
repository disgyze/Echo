using System.Xml.Linq;

namespace Echo.Xmpp.Core.WebSockets
{
    public sealed class XmppWebSocketOpen : XElement
    {
        public static readonly XName ElementName = default;

        public XmppWebSocketOpen() : base(ElementName)
        {
        }
    }
}