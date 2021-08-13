using System;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections
{
    public sealed class AnonymousAuthenticationService : AuthenticationService
    {
        public static readonly string MechanismName = "ANONYMOUS";

        public AnonymousAuthenticationService(IXmppConnection connection, IEventService eventService) : base(connection, eventService)
        {
        }

        protected override async ValueTask OnInitialStateAsync()
        {
            await Connection!.SendAsync(new XmppSaslAuth(MechanismName));
        }

        protected override ValueTask OnChallengeStateAsync(XmppSaslChallenge saslChallenge)
        {
            throw new NotSupportedException();
        }
    }
}