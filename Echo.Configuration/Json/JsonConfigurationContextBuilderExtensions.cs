using System.IO;

namespace Echo.Configuration.Json
{
    public static class JsonConfigurationContextBuilderExtensions
    {
        public static ConfigurationContextBuilder AddJsonStream(this ConfigurationContextBuilder builder, string sourceKey, Stream stream)
        {
            return builder.Add(sourceKey, new JsonStreamConfigurationSourceFactory(stream));
        }
    }
}