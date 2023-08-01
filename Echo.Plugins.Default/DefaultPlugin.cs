using System;
using System.Threading.Tasks;
using Echo.Core;
using Echo.Core.Connections;
using Echo.Core.Extensibility;
using Echo.Core.Extensibility.Commanding;
using Echo.Core.Extensibility.Eventing;
using Echo.Core.Messaging;
using Echo.Core.UI;
using Echo.Core.User;
using Echo.Foundation;

namespace Echo.Plugins.Default
{
    public sealed class DefaultPlugin : IPlugin
    {
        IWindowManager windowManager;
        XmppConnectionServiceManager connectionManager;
        EventService eventService;
        CommandService commandService;
        DisposableContainer subscriptionContainer;

        public string Name => throw new NotImplementedException();
        public string Version => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();
        public Uri Website => throw new NotImplementedException();
        public PluginAuthor Author => throw new NotImplementedException();

        public ValueTask OnLoadAsync(IServiceProvider serviceProvider)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask OnUnloadAsync()
        {
            subscriptionContainer?.Dispose();
            windowManager = null;
            connectionManager = null;
            eventService = null;
            commandService = null;
            return ValueTask.CompletedTask;
        }

        private ValueTask<CommandResult> Echo(CommandArgs c)
        {
            c.Window.Display.ShowEcho(c.Params);
            return ValueTask.FromResult(CommandResult.Stop);
        }

        private async ValueTask<CommandResult> Disconnect(CommandArgs c)
        {
            //if (connectionManager.ActiveConnection is XmppConnection activeConnection && activeConnection.State != InstantMessagingConnectionState.Closed)
            //{
            //    await activeConnection.CloseAsync();
            //}
            return CommandResult.Stop;
        }

        private ValueTask<CommandResult> Window(CommandArgs c)
        {
            if (int.TryParse(c.Params.AsSpan().ParamAt(0), out int index))
            {
                if (windowManager.GetWindow(index) is IWindow window)
                {
                    window.Activate();
                }
                else
                {
                    c.Window.Display.ShowError("/window: Index out of range");
                }
            }
            else
            {
                c.Window.Display.ShowError("/window: Invalid index");
            }
            return ValueTask.FromResult(CommandResult.Stop);
        }

        private ValueTask<CommandResult> Close(CommandArgs c)
        {
            windowManager.ActiveWindow?.Close();
            return ValueTask.FromResult(CommandResult.Stop);
        }

        private async ValueTask<CommandResult> Me(CommandArgs c)
        {
            if (windowManager.GetConversation(c.Window) is Conversation conversation)
            {
                //await conversation.SendActionAsync(c.Params);
            }
            else
            {
                c.Window.Display.ShowError("/me: Can only be called from a conversation window");
            }
            return CommandResult.Stop;
        }

        // /kick [nick] <reason>
        private ValueTask<CommandResult> Kick(CommandArgs c)
        {
            //async ValueTask<CommandResult> KickAsync(MucChannel channel, string nick, string? reason)
            //{
            //    await channel.KickAsync(nick, reason);
            //    return CommandResult.Stop;
            //}

            //var nick = c.Params.AsSpan().ParamAt(0) switch
            //{
            //    ReadOnlySpan<char> { IsEmpty: false } x => x.ToString(),
            //    _ => null
            //};

            //if (nick is not null)
            //{
            //    if (windowManager.GetConversation(c.Window) is MucChannel channel)
            //    {
            //        var reason = c.Params.AsSpan().ParamOnwardAt(1) switch
            //        {
            //            ReadOnlySpan<char> { IsEmpty: false } x => x.ToString(),
            //            _ => null
            //        };

            //        return KickAsync(channel, nick, reason);
            //    }
            //    else
            //    {
            //        c.Window.Display.ShowError("/kick: Can only be used from MUC-channel window");
            //    }
            //}
            //else
            //{
            //    c.Window.Display.ShowError("/kick: No nick specified");
            //}

            return ValueTask.FromResult(CommandResult.Stop);
        }

        private async ValueTask<CommandResult> MassKick(CommandArgs c)
        {
            //if (windowManager.GetConversation(c.Window) is MucChannel channel)
            //{
            //    var reason = c.Params.ParamOnwardAt(0);

            //    await foreach (var participant in channel.GetParticipantsAsync().ConfigureAwait(false))
            //    {
            //        if (await channel.KickAsync(participant, reason).ConfigureAwait(false) is not (ChannelKickResult.Success or ChannelKickResult.ParticipantNotFound))
            //        {
            //            return CommandResult.Stop;
            //        }
            //    }
            //}
            //else
            //{
            //    c.Window.Display.ShowError("/masskick: Can only be used from MUC-channel window");
            //}
            return CommandResult.Stop;
        }

        //private async ValueTask<CommandResult> LeaveAll(CommandArgs c)
        //{
        //    var conversationService = conversationServiceProvider.GetFor(c.Connection);

        //    if (conversationService is not null)
        //    {
        //        var reason = c.Params.ParamOnwardAt(0);

        //        for (int i = 0; i < conversationService.Count; i++)
        //        {
        //            if (conversationService.GetConversation(i) is IChannel channel)
        //            {
        //                await channel.LeaveAsync(reason);
        //            }
        //        }
        //    }

        //    return CommandResult.Stop;
        //}
    }
}