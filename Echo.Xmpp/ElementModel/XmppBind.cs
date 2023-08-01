using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppBind : XElement
	{
		public static readonly XName ElementName = XName.Get("bind", XmppCoreNamespace.Bind);

		public string? JabberId
		{
			get => (string?)Element("jid");
			set => SetElementValue(ElementName.Namespace + "jid", value);
		}

		public string? Resource
		{
			get => (string?)Element("resource");
			set => SetElementValue(ElementName.Namespace + "resource", value);
		}

		public XmppBind() : base(ElementName)
		{
		}

		public XmppBind(string resource) : this()
		{
			Resource = resource;
		}

		public XmppBind(string jabberId, string resource) : this(resource)
        {
			JabberId = jabberId;
        }
	}
}