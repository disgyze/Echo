namespace Echo.Core.Messaging
{
    public readonly struct MucChannelConfiguration
    {
        public bool IsAnonymous { get; }
        public bool IsLogged { get; }
        public bool IsPasswordProtected { get; }

        public MucChannelConfiguration(bool isAnonymous, bool isLogged, bool isPasswordProtected)
        {
            IsAnonymous = isAnonymous;
            IsLogged = isLogged;
            IsPasswordProtected = isPasswordProtected;
        }
    }
}