using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    public interface IEventPublisher<TEvetArgs> where TEvetArgs : struct
    {
        ValueTask<EventResult> PublishAsync(TEvetArgs args);
    }
}