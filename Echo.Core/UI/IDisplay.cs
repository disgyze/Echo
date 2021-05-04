using System;

namespace Echo.Core.UI
{
    public interface IDisplay
    {
        void Clear();
        void ShowOther(string text);
        void ShowError(string text);
        void ShowConnectionInfo(string text);
        void ShowConnectionError(string text);
        void ShowAuthenticationInfo(string text);
        void ShowAuthenticationError(string text);
        void ShowChatMessage(string sender, string target, string text);
        void ShowChatAction(string sender, string target, string text);
        void ShowChannelAction(string channel, string sender, string text);
        void ShowChannelMessage(string channel, string sender, string text);
    }
}