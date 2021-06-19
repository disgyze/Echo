using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IMixChannel : IChannel
    {
        ValueTask<bool> BanAsync(string nick, string? reason = null);
    }
}