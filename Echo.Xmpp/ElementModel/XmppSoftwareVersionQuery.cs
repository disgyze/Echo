using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSoftwareVersionQuery : XElement
	{
		public static readonly XName ElementName = XName.Get("query", XmppExtensionNamespace.SoftwareVersion);

		public XmppSoftwareVersionQuery() : base(ElementName) { }
	}
}