using System.Collections.Generic;

namespace Echo.Xmpp.Client.Authentication
{
	public interface IXmppAuthenticatorFactory
	{
		IXmppAuthenticator Create(IEnumerable<string> mechanisms);
		void Register<TAuthenticator>(string mechanism) where TAuthenticator : IXmppAuthenticator;
	}
}