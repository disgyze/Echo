using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppRoster : XElement
	{
		public static readonly XName ElementName = XName.Get("query", XmppCoreNamespace.Roster);

		public string? Version
		{
			get => (string?)Attribute("ver");
			set => SetAttributeValue("ver", value);
		}

		public IEnumerable<XmppRosterItem> Items
		{
			get => Elements(XmppRosterItem.ElementName).OfType<XmppRosterItem>();
		}

		public XmppRoster() : base(ElementName)
		{
		}

		public XmppRoster(string version) : base(ElementName)
		{
			Version = version;
		}
	}
}