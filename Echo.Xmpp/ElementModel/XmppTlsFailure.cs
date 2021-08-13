using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppTlsFailure : XElement
	{
		public static readonly XName ElementName = XName.Get("failure", CoreNamespaces.Tls);

		public XmppTlsFailure() : base(ElementName)
		{
		}
	}
}