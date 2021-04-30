using System;
using Echo.Xmpp.ElementModel;

namespace Echo.Xmpp.Client.Authentication
{
	public sealed class XmppAuthenticationException : Exception
	{
		public XmppSaslFailureReason Reason
		{
			get;
			private set;
		}

		public XmppAuthenticationException(XmppSaslFailureReason reason)
		{
			Reason = reason;
		}
	}
}