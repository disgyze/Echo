using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppServiceDiscoveryInfo : XElement
	{
		public static readonly XName ElementName = XName.Get("query", XmppCoreNamespace.ServiceDiscoveryInfo);

		public XmppServiceDiscoveryInfo() : base(ElementName)
		{
		}
	}
}