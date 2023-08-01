using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Echo.Configuration.Xml
{
    public sealed class XmlStreamConfigurationSourceFactory : ConfigurationSourceFactory
    {
        static readonly XmlReaderSettings DefaultXmlReaderSettings = new XmlReaderSettings()
        {
            IgnoreComments = true,
            IgnoreWhitespace = true,
            IgnoreProcessingInstructions = true,
            Async = true,
            ValidationType = ValidationType.None,
            ConformanceLevel = ConformanceLevel.Auto
        };

        Stream stream;

        public XmlStreamConfigurationSourceFactory(Stream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public override async ValueTask<ConfigurationSource> LoadAsync(string sourceKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (sourceKey is null)
            {
                throw new ArgumentNullException(nameof(sourceKey));
            }

            using var reader = XmlReader.Create(stream, DefaultXmlReaderSettings);
            var sourceFactory = new XmlReaderConfigurationSourceFactory(reader);
            return await sourceFactory.LoadAsync(sourceKey, cancellationToken).ConfigureAwait(false);
        }
    }
}