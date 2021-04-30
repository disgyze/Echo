using System;

namespace Echo.Core.Messaging
{
    public sealed class ChatStateChangedEventArgs : EventArgs
    {
        public IChat Chat { get; }
        public ChatState State { get; }

        public ChatStateChangedEventArgs(IChat chat, ChatState state)
        {
            Chat = chat;
            State = state;
        }
    }
}