using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface ISupportsCommand
    {
        Task CommandAsync(string name);
    }
}