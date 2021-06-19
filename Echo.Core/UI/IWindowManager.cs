using System;

namespace Echo.Core.UI
{
    public interface IWindowManager
    {
        int Count { get; }
        IWindow ActiveWindow { get; }

        IWindow GetWindow(int windowIndex);
        IWindow GetWindow(Guid windowId);
    }
}