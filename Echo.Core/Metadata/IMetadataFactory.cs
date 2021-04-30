namespace Echo.Core.Metadata
{
    public interface IMetadataFactory
    {
        T Create<T>() where T : IMetadata;
    }
}