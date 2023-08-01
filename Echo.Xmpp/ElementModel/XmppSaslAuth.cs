using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslAuth : XElement
	{
		public static readonly XName ElementName = XName.Get("auth", XmppCoreNamespace.Sasl);

		public string? Mechanism
		{
			get => (string?)Attribute("mechanism");
			set => SetAttributeValue("mechanism", value);
		}

		public XmppSaslAuth() : base(ElementName)
		{
		}

		public XmppSaslAuth(object content) : base(ElementName, content)
		{
		}

		public XmppSaslAuth(string mechanism) : base(ElementName)
		{
			Mechanism = mechanism;
		}

		public XmppSaslAuth(string mechanism, object content) : base(ElementName, content)
		{
			Mechanism = mechanism;
		}
	}
}