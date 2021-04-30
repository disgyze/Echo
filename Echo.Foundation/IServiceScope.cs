namespace Echo.Foundation
{
    public interface IServiceScope
    {
        IReadOnlyServiceManager ServiceManager { get; }
    }
}