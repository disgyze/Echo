using Echo.Core.Connections;
using Echo.Core.Extensibility;
using Echo.Core.UI;
using Echo.Core.User;
using Echo.Foundation;
using Echo.Xmpp.Parser;

namespace Echo.Core
{
    public static class IServiceManagerExtensions
    {
        public static void UseEchoCore(this IServiceManager serviceManager)
        {
            serviceManager

                // Connection events
                .RegisterSingleton<IEvent<ActiveConnectionChangedEventArgs>>(_ => new Event<ActiveConnectionChangedEventArgs>())
                .RegisterSingleton<IEvent<ConnectionStateChangedEventArgs>>(_ => new Event<ConnectionStateChangedEventArgs>())
                .RegisterSingleton<IEvent<ConnectionFailedEventArgs>>(_ => new Event<ConnectionFailedEventArgs>())
                .RegisterSingleton<IEvent<ConnectionErrorEventArgs>>(_ => new Event<ConnectionErrorEventArgs>())

                // UI events
                .RegisterSingleton<IEvent<ApplicationActivatedEventArgs>>(_ => new Event<ApplicationActivatedEventArgs>())
                .RegisterSingleton<IEvent<ApplicationDeactivatedEventArgs>>(_ => new Event<ApplicationDeactivatedEventArgs>())
                .RegisterSingleton<IEvent<ApplicationMaximizedEventArgs>>(_ => new Event<ApplicationMaximizedEventArgs>())
                .RegisterSingleton<IEvent<ApplicationMinimizedEventArgs>>(_ => new Event<ApplicationMinimizedEventArgs>())

                // XML parser
                .RegisterInstance<IXmppElementFactory>(_ => new XmppElementFactory())
                .RegisterInstance<IXmppParser>(serviceProvider =>
                    new XmppParser(
                        serviceProvider.GetService<IXmppElementFactory>()))

                // Connection services
                //.RegisterSingleton<IXmppConnectionManager>(serviceProvider =>
                //    new XmppConnectionManager(
                //        serviceProvider.GetService<IEvent<ActiveConnectionChangedEventArgs>>()))

                .RegisterSingleton<IEventService>(_ => new EventService())
                .RegisterSingleton<IAccountManager>(new AccountManager());
        }
    }
}