using System;
using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppStreamError : XElement
	{
		public static readonly XName ElementName = XName.Get("error", CoreNamespaces.StreamError);

		public XmppStreamErrorReason Reason
		{
			get
			{
				XmppStreamErrorReason reason = XmppStreamErrorReason.Unknown;
				try
				{
					return (XmppStreamErrorReason)Enum.Parse(typeof(XmppStreamErrorReason), (string)Elements().FirstOrDefault(e => e.Name.NamespaceName == CoreNamespaces.StreamError), ignoreCase: true);
				}
				catch
				{
					return reason;
				}
			}
		}

		private XmppStreamErrorReason MapError(XElement element)
		{
			switch (element.Name.LocalName)
			{
				default:
					return XmppStreamErrorReason.Unknown;
			}
		}

		public XmppStreamError() : base(ElementName)
		{
		}

		public XmppStreamError(object content) : base(ElementName, content)
		{
		}

		public XmppStreamError(params object[] content) : base(ElementName, content)
		{
		}

		//public XmppStreamError(XmppStreamErrorReason reason) : this()
		//{
		//	Reason = reason;
		//}
	}
}