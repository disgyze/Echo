using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Xmpp.ElementModel;
using Echo.Xmpp.Parser;

namespace Echo.Xmpp.Client.Authentication
{
	public sealed class XmppPlainAuthenticator : IXmppAuthenticator
	{
		public Task AuthenticateAsync(XmppCoreClient connection, XmppAddress address, string password)
		{
			var completionSource = new TaskCompletionSource();
			AuthenticateAsyncCore(connection, address, password, completionSource);
			return completionSource.Task;
		}

		private async void AuthenticateAsyncCore(XmppCoreClient connection, XmppAddress address, string password, TaskCompletionSource completionSource)
		{
			async void EventElementReceived(object sender, XmppElementEventArgs<XElement> e)
            {
				switch (e.Element)
				{
					case XmppSaslChallenge:
					{
						await connection.SendAsync(
							new XmppSaslResponse(
								Convert.ToBase64String(
									Encoding.UTF8.GetBytes(
										string.Format("\x00{0}\x00{1}", address.ToBare(), password)))));
						break;
					}

					case XmppSaslSuccess:
					{
						completionSource.SetResult();
						connection.XmlElementReceived -= EventElementReceived!;
						break;
					}

					case XmppSaslFailure failure:
					{
						completionSource.SetException(new XmppAuthenticationException(failure.Reason));
						connection.XmlElementReceived -= EventElementReceived!;
						break;
					}
				}
			}

			connection.XmlElementReceived += EventElementReceived!;
			await connection.SendAsync(new XmppSaslAuth(KnownMechanisms.Plaintext));
		}
    }
}