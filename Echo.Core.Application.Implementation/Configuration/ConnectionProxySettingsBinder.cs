using System.Collections.Immutable;
using Echo.Configuration;

namespace Echo.Core.Configuration
{
    public sealed class ConnectionProxySettingsBinder : ConfigurationBinder<ConnectionProxySettings>
    {
        public static readonly string ConfigurationKey = "Proxy";
        static readonly string ProxyItemKey = "Item";
        static readonly string ProxyListKey = "Items";
        static readonly string ProxyCredentialKey = "Credential";

        public override ConnectionProxySettings Bind(ConfigurationNode node)
        {
            if (node is null || node.IsEmpty)
            {
                return ConnectionProxySettings.Default;
            }

            ImmutableArray<ConnectionProxyItem> items = default;

            if (node.GetNode(ProxyListKey) is ConfigurationNode itemsNode && !itemsNode.IsEmpty)
            {
                var itemsBuilder = ImmutableArray.CreateBuilder<ConnectionProxyItem>();

                foreach (var itemNode in itemsNode.Children)
                {
                    if (itemNode.IsEmpty)
                    {
                        continue;
                    }

                    ConnectionProxyCredential? credential = null;

                    if (itemNode.GetNode(ProxyCredentialKey) is ConfigurationNode credentialNode && !credentialNode.IsEmpty)
                    {
                        credential = new ConnectionProxyCredential(
                            credentialNode.GetValueAsString(nameof(ConnectionProxyCredential.UserName), null),
                            credentialNode.GetValueAsString(nameof(ConnectionProxyCredential.Password), null));
                    }

                    ConnectionProxyItem item = new ConnectionProxyItem(
                        itemNode.GetValueAsString(nameof(ConnectionProxyItem.Host)) ?? string.Empty,
                        itemNode.GetValueAsInt32(nameof(ConnectionProxyItem.Port)),
                        itemNode.GetValueAsEnum<ConnectionProxyProtocol>(nameof(ConnectionProxyItem.Protocol)),
                        credential);

                    itemsBuilder.Add(item);
                }

                items = itemsBuilder.ToImmutable();
            }

            return new ConnectionProxySettings(
                node.GetValueAsBoolean(nameof(ConnectionProxySettings.IsEnabled), ConnectionProxySettings.Default.IsEnabled),
                node.GetValueAsBoolean(nameof(ConnectionProxySettings.UseSystemProxy), ConnectionProxySettings.Default.UseSystemProxy),
                items);
        }

        public override ConfigurationNode Bind(ConnectionProxySettings settings)
        {
            if (settings is null || settings == ConnectionProxySettings.Default)
            {
                return ConfigurationNode.Empty;
            }

            var proxyItemsNodeBuilder = new ConfigurationNodeBuilder(ProxyListKey);

            foreach (var item in settings.Items)
            {
                var itemNodeBuilder = new ConfigurationNodeBuilder(ProxyItemKey)
                    .AddProperty(nameof(ConnectionProxyItem.Host), item.Host, null)
                    .AddProperty(nameof(ConnectionProxyItem.Port), item.Port)
                    .AddProperty(nameof(ConnectionProxyItem.Protocol), item.Protocol);

                if (item.Credential is not null)
                {
                    itemNodeBuilder.AddNode(
                        new ConfigurationNodeBuilder(ProxyCredentialKey)
                            .AddProperty(nameof(ConnectionProxyCredential.UserName), item.Credential?.UserName, null)
                            .AddProperty(nameof(ConnectionProxyCredential.Password), item.Credential?.Password, null)
                            .Build());
                }

                proxyItemsNodeBuilder.AddNode(itemNodeBuilder.Build());
            }

            return new ConfigurationNodeBuilder(ConfigurationKey)
                .AddProperty(nameof(ConnectionProxySettings.IsEnabled), settings.IsEnabled, ConnectionProxySettings.Default.IsEnabled)
                .AddProperty(nameof(ConnectionProxySettings.UseSystemProxy), settings.UseSystemProxy, ConnectionProxySettings.Default.UseSystemProxy)
                .AddNode(proxyItemsNodeBuilder.Build())
                .Build();
        }
    }
}