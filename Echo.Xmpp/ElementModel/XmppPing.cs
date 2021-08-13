using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public sealed class XmppPing : XElement
    {
        public static readonly XName ElementName = XName.Get("ping", ExtensionNamespaces.Ping);

        public XmppPing() : base(ElementName) { }
    }
}