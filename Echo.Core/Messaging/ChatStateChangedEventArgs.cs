using System;

namespace Echo.Core.Messaging
{
    public sealed class ChatStateChangedEventArgs : EventArgs
    {
        public IDirectChat Chat { get; }
        public ChatState State { get; }

        public ChatStateChangedEventArgs(IDirectChat chat, ChatState state)
        {
            Chat = chat;
            State = state;
        }
    }
}