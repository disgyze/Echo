using System;
using System.Threading.Tasks;

namespace Echo.Core.UI
{
    public abstract class Window : IWindow
    {
        public abstract Guid Id { get; }
        public abstract bool IsActive { get; }
        public abstract IDisplay Display { get; }
        public abstract WindowKind Kind { get; }
        public abstract object Data { get; set; }

        public abstract void Activate();
        public abstract void Close();
        public abstract ValueTask CommandAsync(string text);
    }
}