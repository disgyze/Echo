using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.Blocklist
{
    public sealed class XmppBlocklistItem : XElement
    {
        public static readonly XName ElementName = XName.Get("item", XmppExtensionNamespace.Blocking);

        public string Jid
        {
            get => (string)Attribute("jid")!;
            set => SetAttributeValue("jid", value);
        }

        public XmppBlocklistItem(string jid) : base(ElementName)
        {
            Jid = jid;
        } 
    }
}