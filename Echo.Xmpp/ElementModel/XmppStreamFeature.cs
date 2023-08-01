using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public sealed class XmppStreamFeature : XElement
	{
		public static readonly XName ElementName = XName.Get("feature", XmppCoreNamespace.Stream);

		public string? Var
		{
			get
			{
				return (string?)Attribute(ElementName.Namespace + "var");
			}
			set
			{
				SetAttributeValue(ElementName.Namespace + "var", value);
			}
		}

		public XmppStreamFeature() : base(ElementName)
		{

		}
	}
}