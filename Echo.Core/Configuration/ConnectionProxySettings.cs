using System.Collections.Immutable;

namespace Echo.Core.Configuration
{
    public sealed record ConnectionProxySettings(bool IsEnabled, bool UseSystemProxy, ImmutableArray<ConnectionProxyItem> Items)
    {
        public static readonly ConnectionProxySettings Default = new ConnectionProxySettings(
            IsEnabled: false,
            UseSystemProxy: false,
            Items: ImmutableArray<ConnectionProxyItem>.Empty);
    }
}