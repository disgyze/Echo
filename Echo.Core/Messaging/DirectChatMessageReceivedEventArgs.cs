using System;

namespace Echo.Core.Messaging
{
    public sealed class DirectChatMessageReceivedEventArgs : EventArgs
    {
        public IDirectChat DirectChat { get; }
        public string Message { get; }
        
        public DirectChatMessageReceivedEventArgs(IDirectChat directChat, string message)
        {
            DirectChat = directChat;
            Message = message;
        }
    }
}