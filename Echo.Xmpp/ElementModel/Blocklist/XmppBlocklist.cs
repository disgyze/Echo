using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace Echo.Xmpp.ElementModel.Blocklist
{
    public sealed class XmppBlocklist : XElement
    {
        public static readonly XName ElementName = XName.Get("blocklist", XmppExtensionNamespace.Blocking);
        public static readonly XmppBlocklist Empty = new XmppBlocklist();

        public IEnumerable<XmppBlocklistItem> Items
        {
            get => Elements().OfType<XmppBlocklistItem>();
        }

        public XmppBlocklist(params XmppBlocklistItem[] items) : base(ElementName, items)
        {
        }
    }
}