using System;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppPresence : XmppStanza
	{
		public static readonly XName ElementName = XName.Get("presence", XmppCoreNamespace.Client);

		public XmppPresenceKind Kind
		{
			get
			{
				Enum.TryParse((string?)Attribute("type"), true, out XmppPresenceKind kind);
				return kind;
			}
			set => SetAttributeValue("type", value != XmppPresenceKind.None ? value.ToString().ToLower() : null);
		}

		public XmppPresenceStatus Status
		{
			get
			{
				Enum.TryParse((string?)Element(Name.Namespace + "show"), true, out XmppPresenceStatus status);
				return status;
			}
			set => SetElementValue(Name.Namespace + "show", value != XmppPresenceStatus.None ? value.ToString().ToLower() : null);
		}

		public string? StatusText
		{
			get => (string?)Element("status");
			set => SetElementValue(Name.Namespace + "status", value);
		}

		public int Priority
		{
			get
			{
				int.TryParse((string?)Element("priority"), out int p);
				return p;
			}
			set => SetElementValue("priority", value != 0 ? value : null);
		}

		public XmppPresence() : base(ElementName)
		{
		}

		public XmppPresence(object content) : base(ElementName, content)
		{
		}

		public XmppPresence(string? sender = null, string? target = null, string? statusText = null, XmppPresenceStatus status = XmppPresenceStatus.None, XmppPresenceKind kind = XmppPresenceKind.None) : base(ElementName)
		{
			Sender = sender;
			Target = target;
			StatusText = statusText;
			Status = status;
			Kind = kind;
		}

		//public XmppPresence(string target) : base(ElementName)
		//{
		//	Target = target;
		//}

		//public XmppPresence(string target, XmppPresenceKind kind) : base(ElementName)
		//{
		//	Target = target;
		//	Kind = kind;
		//}

		//public XmppPresence(string target, XmppPresenceKind kind, XmppPresenceStatus status) : base(ElementName)
		//{
		//	Target = target;
		//	Kind = kind;
		//	Status = status;
		//}

		//public XmppPresence(string target, XmppPresenceKind kind, XmppPresenceStatus status, string statusText) : base(ElementName)
		//{
		//	Target = target;
		//	Kind = kind;
		//	Status = status;
		//	StatusText = statusText;
		//}

		//public XmppPresence(string target, XmppPresenceStatus status) : base(ElementName)
		//{
		//	Target = target;
		//	Status = status;
		//}

		//public XmppPresence(string target, XmppPresenceStatus status, string statusText) : base(ElementName)
		//{
		//	Target = target;
		//	Status = status;
		//	StatusText = statusText;
		//}

		//public XmppPresence(XmppPresenceStatus status, string? statusText = null) : base(ElementName)
		//{
		//	Status = status;
		//	StatusText = statusText;
		//}

		//public XmppPresence(XmppPresenceKind kind, XmppPresenceStatus status, string? statusText = null) : base(ElementName)
		//{
		//	Kind = kind;
		//	Status = status;
		//	StatusText = statusText;
		//}

		//public XmppPresence(XmppPresenceKind kind) : base(ElementName)
		//{
		//	Kind = kind;
		//}

		//public XmppPresence(XmppPresenceKind kind, string statusText) : base(ElementName)
		//{
		//	Kind = kind;
		//	StatusText = statusText;
		//}
	}
}