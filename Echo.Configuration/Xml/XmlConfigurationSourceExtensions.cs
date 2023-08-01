using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Echo.Configuration.Xml
{
    public static class XmlConfigurationSourceExtensions
    {
        public static Task SaveAsXmlAsync(this ConfigurationSource source, XmlWriter writer, CancellationToken cancellationToken = default) 
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (source.Root.IsEmpty)
            {
                return Task.CompletedTask;
            }

            writer.WriteStartDocument();
            SaveAsXml(source.Root, writer);
            writer.WriteEndDocument();

            return writer.FlushAsync();

            static void SaveAsXml(ConfigurationNode node, XmlWriter writer)
            {
                writer.WriteStartElement(node.Key);

                foreach (var (key, value) in node.Properties)
                {
                    writer.WriteAttributeString(key, value?.ToString());
                }

                foreach (var child in node.Children)
                {
                    SaveAsXml(child, writer);
                }

                writer.WriteEndElement();
            }
        }
    }
}