using System.Threading.Tasks;

namespace Echo.Core.UI
{
    public interface IWindow
    {
        bool IsActive { get; }
        IDisplay Display { get; }
        WindowKind Kind { get; }

        void Activate();
        Task CloseAsync();
    }
}