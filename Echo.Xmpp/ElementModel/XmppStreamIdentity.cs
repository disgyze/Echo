using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public sealed class XmppStreamIdentity : XElement
	{
		public static readonly XName ElementName = XName.Get("identity", XmppCoreNamespace.Stream);

		public string? Type
		{
			get => (string?)Attribute(ElementName.Namespace + "type");
			set => SetAttributeValue(ElementName.Namespace + "type", value);
		}

		public string? Category
		{
			get => (string?)Attribute(ElementName.Namespace + "category");
			set => SetAttributeValue(ElementName.Namespace + "category", value);
		}

		public string? IdentityName
		{
			get => (string?)Attribute(ElementName.Namespace + "name");
			set => SetAttributeValue(ElementName.Namespace + "name", value);
		}

		public XmppStreamIdentity() : base(ElementName)
		{
		}
	}
}