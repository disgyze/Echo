using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.Blocklist
{
    public sealed class XmppUnblock : XElement
    {
        public static readonly XName ElementName = XName.Get("unblock", XmppExtensionNamespace.Blocking);
        public static readonly XmppUnblock Empty = new XmppUnblock();

        public XmppUnblock(XmppBlocklistItem? item = null) : base(ElementName)
        {
            if (item is not null)
            {
                Add(item);
            }
        }
    }
}