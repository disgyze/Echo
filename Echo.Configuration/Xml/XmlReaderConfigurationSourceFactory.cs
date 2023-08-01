using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Echo.Configuration.Xml
{
    public sealed class XmlReaderConfigurationSourceFactory : ConfigurationSourceFactory
    {
        XmlReader reader;

        public XmlReaderConfigurationSourceFactory(XmlReader reader)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public override async ValueTask<ConfigurationSource> LoadAsync(string sourceKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            var document = await XDocument.LoadAsync(reader, LoadOptions.None, cancellationToken).ConfigureAwait(false);

            if (document.Root is not null)
            {
                return new ConfigurationSource(sourceKey, CreateNode(document.Root));
            }

            return new ConfigurationSource(sourceKey, ConfigurationNode.Empty);

            static ConfigurationNode CreateNode(XElement element)
            {
                var nodeBuilder = new ConfigurationNodeBuilder(element.Name.LocalName);

                foreach (var attribute in element.Attributes())
                {
                    nodeBuilder.AddProperty(attribute.Name.LocalName, attribute.Value, null);
                }

                foreach (var child in element.Elements())
                {
                    nodeBuilder.AddNode(CreateNode(child));
                }

                return nodeBuilder.Build();
            }
        }
    }
}