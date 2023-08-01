using System;
using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppStreamError : XElement
	{
		public static readonly XName ElementName = XName.Get("error", XmppCoreNamespace.StreamError);

		public XmppStreamErrorReason Reason
		{
			get
			{
				return Enum.TryParse<XmppStreamErrorReason>((string?)Elements().FirstOrDefault(e => e.Name.NamespaceName == XmppCoreNamespace.StreamError), true, out var result) ? result : XmppStreamErrorReason.Unknown;
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