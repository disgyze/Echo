using System;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections.Authentication
{
    //public sealed class AnonymousAuthenticationService : AuthenticationService
    //{
    //    public static readonly string MechanismName = "ANONYMOUS";

    //    public AnonymousAuthenticationService(EventService eventService, XmppConnectionStream stream) : base(eventService, stream)
    //    {
    //    }

    //    protected override async ValueTask OnInitialAsync()
    //    {
    //        if (GetStream() is XmppConnectionStream stream)
    //        {
    //            await stream.WriteAsync(new XmppSaslAuth());
    //        }
    //    }

    //    protected override ValueTask OnChallengeAsync(XmppSaslChallenge challenge)
    //    {
    //        throw new NotSupportedException();
    //    }
    //}
}