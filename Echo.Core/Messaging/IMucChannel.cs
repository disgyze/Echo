using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IMucChannel : IChannel
    {
        Task<bool> BanAsync(string nick, string? reason = null);
        Task<bool> KickAsync(string nick, string? reason = null);
    }
}