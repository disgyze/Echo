using System;
using Echo.Xmpp;

namespace Echo.Core.Messaging
{
    public sealed class ChatMessageReceivedEventArgs : EventArgs
    {
        public IChat Chat { get; }
        public XmppAddress Sender { get; }
        public XmppAddress Target { get; }
        public string? Text { get; }

        public ChatMessageReceivedEventArgs(IChat chat, XmppAddress sender, XmppAddress target, string? text)
        {
            Chat = chat;
            Sender = sender;
            Target = target;
            Text = text;
        }
    }
}