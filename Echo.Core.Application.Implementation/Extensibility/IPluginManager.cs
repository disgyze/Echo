using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IPluginManager
    {
        IEnumerable<IPlugin> Plugins { get; }

        ValueTask<bool> LoadAsync(string path);
        ValueTask<bool> UnloadAsync(Guid pluginId);
        ValueTask<bool> ReloadAsync(Guid pluginId);
    }
}