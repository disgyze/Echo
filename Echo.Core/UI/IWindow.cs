using System;
using Echo.Core.Extensibility;

namespace Echo.Core.UI
{
    public interface IWindow
    {
        Guid Id { get; }
        bool IsActive { get; }
        IDisplay Display { get; }
        WindowKind Kind { get; }

        void Activate();
        void Close();
    }
}