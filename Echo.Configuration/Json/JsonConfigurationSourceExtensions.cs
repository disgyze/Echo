using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Configuration.Json
{
    public static class JsonConfigurationSourceExtensions
    {
        public static Task SaveAsJsonAsync(this ConfigurationSource source, Utf8JsonWriter writer, CancellationToken cancellationToken = default)
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

            SaveAsJson(source.Root, writer, true);
            return writer.FlushAsync(cancellationToken);

            static void SaveAsJson(ConfigurationNode node, Utf8JsonWriter writer, bool isRoot)
            {
                if (isRoot)
                {
                    writer.WriteStartObject();
                }
                else
                {
                    writer.WriteStartObject(node.Key);
                }

                foreach (var (key, value) in node.Properties)
                {
                    writer.WriteString(key, value?.ToString());
                }

                foreach (var child in node.Children)
                {
                    SaveAsJson(child, writer, false);
                }

                writer.WriteEndObject();
            }
        }
    }
}