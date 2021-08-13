using System;
using Echo.Xmpp.Client.Authentication;

namespace Echo.Xmpp.Client
{
	public sealed class AuthenticationEventArgs : EventArgs
	{
		public bool IsSuccess
		{
			get;
			private set;
		}

		public XmppAuthenticationException Error
		{
			get;
			private set;
		}

		public AuthenticationEventArgs(bool isSuccess, XmppAuthenticationException error = null)
		{
			IsSuccess = isSuccess;
			Error = error;
		}
	}
}