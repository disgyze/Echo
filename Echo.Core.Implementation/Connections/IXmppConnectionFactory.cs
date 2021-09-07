using Echo.Core.User;

namespace Echo.Core.Connections
{
    public interface IXmppConnectionFactory
    {
        IXmppConnection Create(IAccount account);
    }
}