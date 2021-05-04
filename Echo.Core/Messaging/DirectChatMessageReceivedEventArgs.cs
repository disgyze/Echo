using System;

namespace Echo.Core.Messaging
{
    public sealed class DirectChatMessageReceivedEventArgs : EventArgs
    {
        public IDirectChat DirectChat { get; }
        public ChatMessage Message { get; }
        
        public DirectChatMessageReceivedEventArgs(IDirectChat directChat, ChatMessage message)
        {
            DirectChat = directChat;
            Message = message;
        }
    }
}