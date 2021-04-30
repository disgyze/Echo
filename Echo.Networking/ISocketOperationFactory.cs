using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface ISocketOperationFactory
    {
        ISocketReader CreateReadOperation(Socket socket, Stream stream = default, TaskScheduler scheduler = default);
        ISocketWriter CreateWriteOperation(Socket socket, Stream stream = default, TaskScheduler scheduler = default);
    }
}