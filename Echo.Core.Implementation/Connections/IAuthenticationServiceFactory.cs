namespace Echo.Core.Connections
{
    public interface IAuthenticationServiceFactory
    {
        IAuthenticationService Create(string mechanism);
    }
}