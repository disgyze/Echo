using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppStreamFeatureCollection : XElement
	{
		public static readonly XName ElementName = XName.Get("features", XmppCoreNamespace.Stream);

		public XmppStreamFeatureCollection() : base(ElementName)
		{
		}
	}
}