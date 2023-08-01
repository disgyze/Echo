using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslFailure : XElement
	{
		public static readonly XName ElementName = XName.Get("failure", XmppCoreNamespace.Sasl);

		public XmppSaslFailureReason Reason
		{
			get
			{
				return MapReason(Elements().FirstOrDefault());
			}
		}

		public XmppSaslFailure() : base(ElementName)
		{
		}

		public XmppSaslFailure(object content) : base(ElementName, content)
		{
		}

		public XmppSaslFailure(params object[] content) : base(ElementName, content)
		{
		}

		private XmppSaslFailureReason MapReason(XElement? element)
		{
			if (element == null)
			{
				return XmppSaslFailureReason.Unknown;
			}

			switch (element.Name.LocalName)
			{
				case "aborted": return XmppSaslFailureReason.Aborted;
				case "account-disabled": return XmppSaslFailureReason.AccountDisabled;
				case "credentials-expired": return XmppSaslFailureReason.CredentialsExpired;
				case "encryption-required": return XmppSaslFailureReason.EncryptionRequired;
				case "incorrect-encoding": return XmppSaslFailureReason.IncorrectEncoding;
				case "invalid-authzid": return XmppSaslFailureReason.InvalidAuthzid;
				case "invalid-mechanism": return XmppSaslFailureReason.InvalidMechanism;
				case "malformed-request": return XmppSaslFailureReason.MalformedRequest;
				case "mechanism-too-weak": return XmppSaslFailureReason.MechanismTooWeak;
				case "not-authorized": return XmppSaslFailureReason.NotAuthorized;
				case "temporary-auth-failure": return XmppSaslFailureReason.TemporaryAuthFailure;
				default: return XmppSaslFailureReason.Unknown;
			}
		}
	}
}