using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Configuration.Json
{
    public sealed class JsonStreamConfigurationSourceFactory : ConfigurationSourceFactory
    {
        Stream stream;

        public JsonStreamConfigurationSourceFactory(Stream stream)
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

            using var document = await JsonDocument.ParseAsync(stream, new JsonDocumentOptions() { CommentHandling = JsonCommentHandling.Skip }).ConfigureAwait(false);
            return new ConfigurationSource(sourceKey, CreateNode(string.Empty, document.RootElement));

            static ConfigurationNode CreateNode(string name, JsonElement root)
            {
                var nodeBuilder = new ConfigurationNodeBuilder(name);

                foreach (var property in root.EnumerateObject())
                {
                    switch (property.Value.ValueKind)
                    {
                        case JsonValueKind.Object:
                        {
                            nodeBuilder.AddNode(CreateNode(property.Name, property.Value));
                            break;
                        }

                        case JsonValueKind.String or JsonValueKind.Number:
                        {
                            nodeBuilder.AddProperty(property.Name, property.Value.GetString());
                            break;
                        }

                        case JsonValueKind.True or JsonValueKind.False:
                        {
                            nodeBuilder.AddProperty(property.Name, property.Value.GetBoolean());
                            break;
                        }

                        case JsonValueKind.Null or JsonValueKind.Undefined:
                        {
                            nodeBuilder.AddProperty(property.Name, (object?)null);
                            break;
                        }
                    }
                }

                return nodeBuilder.Build();
            }
        }
    }
}