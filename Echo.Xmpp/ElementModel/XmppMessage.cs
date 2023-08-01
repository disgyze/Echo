using System;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppMessage : XmppStanza
	{
		public static readonly XName ElementName = XName.Get("message", XmppCoreNamespace.Client);

		public string? Body
		{
			get => (string?)Element(Name.Namespace + "body");
			set => SetElementValue(Name.Namespace + "body", value);
		}

		public string? Subject
		{
			get => (string?)Element(Name.Namespace + "subject");
			set => SetElementValue(Name.Namespace + "subject", value);
		}

		public XmppMessageKind Kind
		{
			get
			{
				Enum.TryParse((string?)Attribute("type"), true, out XmppMessageKind kind);
				return kind;
			}
			set => SetAttributeValue("type", value != XmppMessageKind.Normal ? value.ToString().ToLower() : null);
		}

		public XmppMessage() : base(ElementName)
		{
		}

		public XmppMessage(params object[] content) : base(ElementName, content)
		{
		}

		public XmppMessage(XmppMessageKind kind = XmppMessageKind.Normal, string? sender = null, string? target = null, string? body = null, string? id = null, params object[] content) : base(ElementName, content)
		{
			Kind = kind;
			Sender = sender;
			Target = target;
			Body = body;
			Id = id;
		}
	}
}