using System.Threading.Tasks;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public interface IXmppStream
    {
        Task WriteAsync(XElement element);
        Task OpenAsync(string? domain = null);
        Task CloseAsync();
    }
}