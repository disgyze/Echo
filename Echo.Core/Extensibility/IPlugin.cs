using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }
        string Description { get; }
        Uri Website { get; }
        PluginAuthor Author { get; }

        ValueTask OnLoadAsync(IServiceProvider serviceProvider);
        ValueTask OnUnloadAsync();
    }
}