using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        Uri Website { get; }
        PluginAuthor Author { get; }

        Task OnLoadAsync(IServiceProvider serviceProvider);
        Task OnUnloadAsync();
    }
}