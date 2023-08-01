using System;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppIQ : XmppStanza
	{
		public static readonly XName ElementName = XName.Get("iq", XmppCoreNamespace.Client);

		public XmppIQKind Kind
		{
			get
			{
				Enum.TryParse((string)Attribute("type")!, true, out XmppIQKind kind);
				return kind;
			}
			set => SetAttributeValue("type", value.ToString().ToLower());
		}

		public XmppIQ() : base(ElementName)
		{
		}

		public XmppIQ(XmppIQKind kind, params object[] content) : base(ElementName, content)
        {
            Kind = kind;
        }

        public XmppIQ(XmppIQKind kind, string? id, object content) : base(ElementName, content)
		{
			Kind = kind;
			Id = id;
		}

		public XmppIQ(XmppIQKind kind, string? id, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Id = id;
		}

		public XmppIQ(XmppIQKind kind, string? id, string? target, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Id = id;
			Target = target;
		}

		public XmppIQ(XmppIQKind kind, string? id, string? sender, string? target) : base(ElementName)
		{
			Kind = kind;
			Id = id;
			Sender = sender;
			Target = target;
		}

		public XmppIQ(XmppIQKind kind, string? id, string? sender, string? target, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Id = id;
			Sender = sender;
			Target = target;
		}
	}
}