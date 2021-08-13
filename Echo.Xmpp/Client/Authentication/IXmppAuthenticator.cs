using System.Threading.Tasks;

namespace Echo.Xmpp.Client.Authentication
{
	public interface IXmppAuthenticator
	{
		Task AuthenticateAsync(XmppCoreClient connection, XmppAddress address, string password);
	}
}