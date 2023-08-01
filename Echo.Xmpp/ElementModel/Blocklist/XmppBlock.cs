using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.Blocklist
{
    public sealed class XmppBlock : XElement
    {
        public static readonly XName ElementName = XName.Get("block", XmppExtensionNamespace.Blocking);

        public XmppBlock(XmppBlocklistItem item) : base(ElementName)
        {
            Add(item);
        }
    }
}