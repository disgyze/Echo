using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public abstract class PluginManager
    {
        public abstract IEnumerable<IPlugin> GetPlugins();
        public abstract ValueTask<bool> LoadAsync(string? fileName = null, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UnloadAsync(IPlugin plugin, CancellationToken cancellationToken = default);
    }
}