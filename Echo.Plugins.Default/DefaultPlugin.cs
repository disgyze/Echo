using System;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Core.User;
using Echo.Foundation;
using Echo.Core.UI;
using Echo.Core.Client;

namespace Echo.Plugins.Default
{
    using EventResult = Echo.Core.Extensibility.EventResult;
    using CommandResult = Echo.Core.Extensibility.CommandResult;

    public sealed class DefaultPlugin : IPlugin
    {
        IWindowManager windowManager = null;
        IXmppClientManager clientManager = null;
        IEventService eventService = null;
        ICommandService commandService = null;
        DisposableContainer subscriptionContainer = null;

        public string Name => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();
        public Uri Website => throw new NotImplementedException();
        public PluginAuthor Author => throw new NotImplementedException();

        public Task OnLoadAsync(IServiceProvider serviceProvider)
        {            
            subscriptionContainer = new DisposableContainer(
                eventService.RegisterEvent<AccountPresenceChangedEventArgs>(EventAccountPresenceChanged),
                commandService.RegisterCommand("echo", Echo));

            return Task.CompletedTask;
        }

        public Task OnUnloadAsync()
        {
            subscriptionContainer.Dispose();
            return Task.CompletedTask;
        }

        private Task<EventResult> EventAccountPresenceChanged(AccountPresenceChangedEventArgs args)
        {
            return Task.FromResult(EventResult.Continue);
        }

        private Task<CommandResult> Echo(CommandArgs args)
        {
            var window = windowManager.GetWindow(args.WindowId);
            window.Display.ShowOther(args.Params);
            return Task.FromResult(CommandResult.Stop);
        }
    }
}