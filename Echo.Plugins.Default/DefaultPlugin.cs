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
    using CommandResult = Echo.Core.Extensibility.CommandResult;
    using EventResult = Echo.Core.Extensibility.EventResult;

    public sealed class DefaultPlugin : IPlugin
    {
        IWindowManager windowManager = null;
        IChannelManager channelManager = null;
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
                commandService.RegisterCommand("echo", Echo),
                commandService.RegisterCommand("invite", Invite));

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
                if (activeConnection.State != ConnectionState.Disconnected)
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
        
        // /kick [nick/address] <reason>
        private async ValueTask<CommandResult> Kick(CommandArgs c)
        {
            string nick = c.Params.ParamAt(0);

            if (nick == string.Empty)
            {
                c.Window.Display.ShowError("/kick: No nick or address specified");
                return CommandResult.Stop;
            }

            string reason = c.Params.ParamOnwardAt(1);

            if (XmppUri.TryCreate(nick, out var address))
            {
                await channelManager.KickAsync(c.Connection, address, reason);
            }
            else
            {
                if (channelManager.GetChannel(c.Window.Id) is IMucChannel channel)
                {
                    await channel.KickAsync(nick, reason);
                }
                else
                {
                    c.Window.Display.ShowError("/kick: Can only be used from MUC-channel window");
                }
            }

            return CommandResult.Stop;
        }

        private async ValueTask<CommandResult> Join(CommandArgs c)
        {
            string address = c.Params.ParamAt(0);

            return CommandResult.Stop;
        }

        private async ValueTask<CommandResult> Invite(CommandArgs c)
        {
            var channelAddressParameterSpecified = true;

            if (c.Connection.State != ConnectionState.Connected)
            {
                c.Window.Display.ShowError("Not connected");
                return CommandResult.Stop;
            }

            if (!XmppUri.TryCreate(c.Params.ParamAt(1), out var userAddress))
            {
                c.Window.Display.ShowError("/join: User address is invalid or not specified");
                return CommandResult.Stop;
            }

            if (!XmppUri.TryCreate(c.Params.ParamAt(2), out var channelAddress))
            {
                var channel = channelManager.GetChannel(c.Window.Id);

                if (channel == null)
                {
                    c.Window.Display.ShowError("/join: Can only be used from channel window");
                    return CommandResult.Stop;
                }

                channelAddress = channel.Address;
                channelAddressParameterSpecified = false;
            }

            var reason = c.Params.ParamOnwardAt(channelAddressParameterSpecified ? 3 : 2);

            c.Window.Display.ShowChannelInvite(userAddress, channelAddress, reason);
            await channelManager.InviteAsync(c.Connection, userAddress, channelAddress, reason);

            return CommandResult.Stop;
        }
    }
}