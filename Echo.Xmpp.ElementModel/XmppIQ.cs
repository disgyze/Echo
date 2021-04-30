using System;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppIQ : XmppStanza
	{
		public static readonly XName ElementName = XName.Get("iq", CoreNamespaces.Client);

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

		public XmppIQ(XmppIQKind kind, object content, bool generateId = false) : base(ElementName, content)
		{
			Kind = kind;

			if (generateId)
			{
				Id = GenerateId();
			}
		}

		public XmppIQ(XmppIQKind kind, bool generateId = false, params object[] content) : base(ElementName, content)
		{
			Kind = kind;

			if (generateId)
			{
				Id = GenerateId();
			}
		}

		public XmppIQ(XmppIQKind kind, string target, bool generateId = false, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Target = target;

			if (generateId)
			{
				Id = GenerateId();
			}
		}

		public XmppIQ(XmppIQKind kind, string sender, string target, bool generateId = false, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Sender = sender;
			Target = target;

			if (generateId)
			{
				Id = GenerateId();
			}
		}
	}
}