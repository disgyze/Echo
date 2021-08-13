using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppServiceDiscoveryCollection : XElement
	{
		public static readonly XName ElementName = XName.Get("query", CoreNamespaces.ServiceDiscoveryItems);

		public XmppServiceDiscoveryCollection() : base(ElementName)
		{
		}
	}
}