using System;
using System.Threading.Tasks;

namespace Echo.Core.UI
{
    public interface IWindowManager
    {
        int Count { get; }
        IWindow ActiveWindow { get; }

        IWindow GetWindow(int windowIndex);
        IWindow GetWindow(Guid windowId);        
        Task CloseAsync(int windowIndex);
        Task CloseAsync(Guid windowId);
    }
}