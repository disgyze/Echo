namespace Echo.Core.Extensibility.Eventing
{
    internal abstract class EventChannel
    {
        public abstract Event<TEventArgs> GetEvent<TEventArgs>() where TEventArgs : struct;
    }
}