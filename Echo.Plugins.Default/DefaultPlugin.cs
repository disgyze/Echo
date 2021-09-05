using System;
using System.Threading.Tasks;
using Echo.Core;
using Echo.Core.Connections;
using Echo.Core.Extensibility;
using Echo.Core.Messaging;
using Echo.Core.UI;
using Echo.Core.User;
using Echo.Foundation;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Plugins.Default
{
    public sealed class DefaultPlugin : IPlugin
    {
        IWindowManager windowManager = null;
        IXmppConnectionManager connectionManager = null;
        IEventService eventService = null;
        ICommandService commandService = null;
        DisposableContainer subscriptionContainer = null;

        public string Name => throw new NotImplementedException();
        public string Version => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();
        public Uri Website => throw new NotImplementedException();
        public PluginAuthor Author => throw new NotImplementedException();

        public ValueTask OnLoadAsync(IServiceProvider serviceProvider)
        {            
            subscriptionContainer = new DisposableContainer(
                eventService.RegisterEvent<AccountPresenceChangedEventArgs>(EventAccountPresenceChanged),
                commandService.RegisterCommand("echo", Echo));

            return ValueTask.CompletedTask;
        }

        public ValueTask OnUnloadAsync()
        {
            subscriptionContainer.Dispose();
            return ValueTask.CompletedTask;
        }

        private ValueTask<EventResult> EventAccountPresenceChanged(AccountPresenceChangedEventArgs args)
        {
            return ValueTask.FromResult(EventResult.Continue);
        }

        private ValueTask<CommandResult> Echo(CommandArgs c)
        {
            c.Window.Display.ShowEcho(c.Params);
            return ValueTask.FromResult(CommandResult.Stop);
        }

        private async ValueTask<CommandResult> Disconnect(CommandArgs c)
        {
            var activeConnection = connectionManager.ActiveConnection;

            if (activeConnection != null)
            {
                if (activeConnection.ConnectionState != ConnectionState.Closed)
                {

                    // If no params provided, close the active connection
                    if (c.Params == null)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    
                }
            }

            return CommandResult.Stop;
        }
        
        private ValueTask<CommandResult> Window(CommandArgs c)
        {
            if (int.TryParse(c.Params.ParamAt(0), out int index))
            {
                IWindow window = windowManager.GetWindow(index);

                if (window != null)
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
            if (windowManager.GetConversation(c.Window) is IConversation conversation)
            {
                await conversation.SendActionAsync(c.Params);
            }
            else
            {
                c.Window.Display.ShowError("/me: Can only be called from a conversation window");
            }
            return CommandResult.Stop;
        }

        // /kick [nick] <reason>
        private async ValueTask<CommandResult> Kick(CommandArgs c)
        {
            string nick = c.Params.ParamAt(0);

            if (nick == string.Empty)
            {
                c.Window.Display.ShowError("/kick: No nick specified");
                return CommandResult.Stop;
            }

            if (windowManager.GetConversation(c.Window) is IMucChannel channel)
            {
                var reason = c.Params.ParamOnwardAt(1);
                await channel.KickAsync(nick, reason);
            }
            else
            {
                c.Window.Display.ShowError("/kick: Can only be used from MUC-channel window");
            }

            return CommandResult.Stop;
        }

        private async ValueTask<CommandResult> MassKick(CommandArgs c)
        {
            if (windowManager.GetConversation(c.Window) is IMucChannel channel)
            {
                string reason = c.Params.ParamOnwardAt(0);
                IChannelMember member = null;

                for (int i = 0; i < channel.MemberCount; i++)
                {
                    member = channel.GetMember(i);

                    if (member == null)
                    {
                        break;
                    }

                    await channel.KickAsync(member.Nick, reason);
                }
            }
            else
            {
                c.Window.Display.ShowError("/masskick: Can only be used from MUC-channel window");
            }

            return CommandResult.Stop;
        }

        private async ValueTask<CommandResult> LeaveAll(CommandArgs c)
        {
            string reason = c.Params.ParamOnwardAt(0);
            IChannel channel = null;
            IChannelService channelService = c.Connection.GetService<IChannelService>();

            for (int i = 0; i < channelService.Count; i++)
            {
                channel = channelService.GetChannel(i);

                // No channel found, exit
                if (channel == null)
                {
                    break;
                }

                await channel.LeaveAsync(reason);
            }

            return CommandResult.Stop;
        }

        private async ValueTask<CommandResult> Join(CommandArgs c)
        {
            string address = c.Params.ParamAt(0);

            return CommandResult.Stop;
        }

        private ValueTask<CommandResult> MassEcho(CommandArgs c)
        {
            for (int i = 0; i < windowManager.Count; i++)
            {
                var window = windowManager.GetWindow(i);

                if (window == null)
                {
                    break;
                }

                window.Display.ShowEcho(c.Params);
            }
            return ValueTask.FromResult(CommandResult.Stop);
        }
    }
}