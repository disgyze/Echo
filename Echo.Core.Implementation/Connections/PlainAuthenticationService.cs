using System;
using System.Text;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections
{
    public sealed class PlainAuthenticationService : AuthenticationService
    {
        public static string MechanismName = "PLAIN";

        public PlainAuthenticationService(IXmppConnection connection, IEventService eventService) : base(connection, eventService)
        {
        }

        protected override async ValueTask OnInitialStateAsync()
        {
            await Connection!.Stream.WriteAsync(new XmppSaslAuth(MechanismName));
        }

        protected override async ValueTask OnChallengeStateAsync(XmppSaslChallenge saslChallenge)
        {
            var account = Connection!.Account;

            await Connection.Stream.WriteAsync(
                new XmppSaslResponse(
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(
                            string.Format("\x00{0}\x00{1}", account.Address.ToBareString(), account.Password)))));
        }
    }
}