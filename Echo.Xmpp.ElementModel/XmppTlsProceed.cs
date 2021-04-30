using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppTlsProceed : XElement
	{
		public static readonly XName ElementName = XName.Get("proceed", CoreNamespaces.Tls);

		public XmppTlsProceed() : base(ElementName)
		{
		}
	}
}