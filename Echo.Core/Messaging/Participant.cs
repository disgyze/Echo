using Echo.Core.User;

namespace Echo.Core.Messaging
{
    public abstract class Participant
    {
        public Channel Channel { get; }
        public XmppAddress Address { get; } 
        public string Nick { get; internal set; } 
        public Presence Presence { get; internal set; }

        protected Participant(Channel channel, XmppAddress address, string nick, Presence presence)
        {
            Channel = channel;
            Address = address;
            Nick = nick;
            Presence = presence;
        }
    }
}