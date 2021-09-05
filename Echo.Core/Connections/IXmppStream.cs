using System.Threading.Tasks;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public interface IXmppStream
    {
        ValueTask WriteAsync(XElement element);
        ValueTask OpenAsync(string domain);
        ValueTask CloseAsync();
    }
}