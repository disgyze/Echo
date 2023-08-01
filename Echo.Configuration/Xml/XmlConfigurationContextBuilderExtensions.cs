using System.IO;

namespace Echo.Configuration.Xml
{
    public static class XmlConfigurationContextBuilderExtensions
    {
        public static ConfigurationContextBuilder AddXmlStream(this ConfigurationContextBuilder builder, string sourceKey, Stream stream)
        {
            return builder.Add(sourceKey, new XmlStreamConfigurationSourceFactory(stream));
        }
    }
}