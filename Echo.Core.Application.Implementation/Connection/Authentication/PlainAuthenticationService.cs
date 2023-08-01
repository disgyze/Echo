using System;
using System.Text;
using Echo.Core.Extensibility.Eventing;
using Echo.Core.User;
using Echo.Xmpp.Connections;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections.Authentication
{
    public sealed class PlainAuthenticationService : AuthenticationService
    {
        public static readonly string MechanismName = "PLAIN";

        public PlainAuthenticationService(Xmpp.Connections.XmppConnection connection, EventService eventService) : base(connection, eventService)
        {
        }

        protected override XmppSaslAuth GetXmppSaslAuth()
        {
            return new XmppSaslAuth(MechanismName);
        }

        protected override XmppSaslResponse GetXmppSaslResponse(AccountCredential? credential, XmppSaslChallenge challenge)
        {
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            if (challenge is null)
            {
                throw new ArgumentNullException(nameof(challenge));
            }

            return new XmppSaslResponse(
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        string.Format("\x00{0}\x00{1}", credential.AccountAddress, credential.Password))));
        }
    }
}