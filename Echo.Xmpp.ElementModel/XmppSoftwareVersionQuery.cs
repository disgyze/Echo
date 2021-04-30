using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSoftwareVersionQuery : XElement
	{
		public static readonly XName ElementName = XName.Get("query", ExtensionNamespaces.SoftwareVersion);

		public XmppSoftwareVersionQuery() : base(ElementName) { }
	}
}