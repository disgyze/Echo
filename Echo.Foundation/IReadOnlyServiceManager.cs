namespace Echo.Foundation
{
    public interface IReadOnlyServiceManager
    {
        TService GetService<TService>();
    }
}