using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppRosterItem : XElement
	{
		public static readonly XName ElementName = XName.Get("item", CoreNamespaces.Roster);

		public bool Approved
		{
			get => (bool)Attribute("approved");
			set => SetAttributeValue("approved", value ? value : (object)null);
		}

		public string ItemName
		{
			get => (string)Attribute("name");
			set => SetAttributeValue("name", value);
		}

		public XmppRosterItemPendingKind Pending
		{
			get
			{
				Enum.TryParse((string)Attribute("ask"), true, out XmppRosterItemPendingKind pendingKind);
				return pendingKind;
			}
			set => SetAttributeValue("ask", value != XmppRosterItemPendingKind.None ? value.ToString().ToLower() : null);
		}

		public XmppRosterItemSubscription Subscription
		{
			get
			{
				Enum.TryParse((string)Attribute("subscription"), true, out XmppRosterItemSubscription subscription);
				return subscription;
			}
			set => SetAttributeValue("subscription", value.ToString().ToLower());
		}

		public string JabberId
		{
			get => (string)Attribute("jid");
			set => SetAttributeValue("jid", value);
		}

		public IEnumerable<string> Groups
		{
			get => Elements(Name.Namespace + "group").Select(e => e.Value);
		}

		public XmppRosterItem() : base(ElementName)
		{
		}

		public void AddGroup(string name)
		{
			Add(new XElement("group", name));
		}

		public void RemoveGroup(string name)
		{
			Elements(name).Remove();
		}
	}
}