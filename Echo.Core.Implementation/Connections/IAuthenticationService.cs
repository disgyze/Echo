using System;
using System.Threading.Tasks;

namespace Echo.Core.Connections
{
    public interface IAuthenticationService : IDisposable, IAsyncDisposable
    {
        ValueTask<AuthenticationResult> AuthenticateAsync();
    }
}