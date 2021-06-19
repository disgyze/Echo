using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface ISupportsCommand
    {
        ValueTask CommandAsync(string text);
    }
}