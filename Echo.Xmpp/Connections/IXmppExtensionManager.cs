namespace Echo.Xmpp.Connections
{
    public interface IXmppExtensionManager
    {
        IXmppExtensionManager Register<TExtension>(TExtension extension) where TExtension : IXmppExtension;
        IXmppExtensionManager Unregister<TExtension>() where TExtension : IXmppExtension;
    }
}