using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslSuccess : XElement
	{
		public static readonly XName ElementName = XName.Get("success", CoreNamespaces.Sasl);

		public XmppSaslSuccess() : base(ElementName)
		{
		}
	}
}