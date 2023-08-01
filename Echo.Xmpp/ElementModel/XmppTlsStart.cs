using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppTlsStart : XElement
	{
		public static readonly XName ElementName = XName.Get("starttls", XmppCoreNamespace.Tls);

		public XmppTlsStart() : base(ElementName)
		{
		}
	}
}