using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public sealed class PluginManager : IPluginManager
    {
        public IEnumerable<IPlugin> Plugins => throw new NotImplementedException();

        public ValueTask<bool> LoadAsync(string path)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> ReloadAsync(Guid pluginId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> UnloadAsync(Guid pluginId)
        {
            throw new NotImplementedException();
        }
    }
}