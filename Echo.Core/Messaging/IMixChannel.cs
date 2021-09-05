using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IMixChannel : IChannel
    {
        Task<bool> BanAsync(string nick, string? reason = null);
    }
}