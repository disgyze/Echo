using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppStreamFeatureCollection : XElement
	{
		public static readonly XName ElementName = XName.Get("features", CoreNamespaces.Stream);

		public XmppStreamFeatureCollection() : base(ElementName)
		{
		}
	}
}