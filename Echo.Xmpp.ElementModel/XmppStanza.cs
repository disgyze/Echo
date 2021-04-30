using System;
using System.Text;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public abstract class XmppStanza : XElement
	{
		static Random random = new Random();

		public string? Id
		{
			get => (string?)Attribute("id");
			set => SetAttributeValue("id", value);
		}

		public string? Sender
		{
			get => (string?)Attribute("from");
			set => SetAttributeValue("from", value);
		}

		public string? Target
		{
			get => (string?)Attribute("to");
			set => SetAttributeValue("to", value);
		}

		protected XmppStanza(XElement other) : base(other)
		{
		}

		protected XmppStanza(XStreamingElement other) : base(other)
		{
		}

		protected XmppStanza(XName name) : base(name)
		{
		}

		protected XmppStanza(XName name, object content) : base(name, content)
		{
		}

		protected XmppStanza(XName name, params object[] content) : base(name, content)
		{
		}

		public static string GenerateId()
		{
			StringBuilder sb = new StringBuilder(10);

			for (int i = 0; i < 10; i++)
			{
				sb.Append(random.Next(10));
			}

			return sb.ToString();
		}
	}
}