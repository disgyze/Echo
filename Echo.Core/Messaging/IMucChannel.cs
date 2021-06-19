using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public interface IMucChannel : IChannel
    {
        ValueTask<bool> BanAsync(string nick, string? reason = null);
        ValueTask<bool> KickAsync(string nick, string? reason = null);
    }
}