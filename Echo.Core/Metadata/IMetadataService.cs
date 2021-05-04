namespace Echo.Core.Metadata
{
    public interface IMetadataService
    {
        T Create<T>() where T : IMetadata;
        IMetadataService Register<T>();
    }
}