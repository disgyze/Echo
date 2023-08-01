using System;
using Echo.Configuration;

namespace Echo.Core.Configuration
{
    public sealed class ConnectionSettingsBinder : ConfigurationBinder<ConnectionSettings>
    {
        public static readonly string ConfigurationKey = "Connection";

        ConnectionRetryPolicyBinder retryPolicyBinder = new ConnectionRetryPolicyBinder();
        ConnectionProxySettingsBinder proxySettingsBinder = new ConnectionProxySettingsBinder();
        ConnectionSecuritySettingsBinder securitySettingsBinder = new ConnectionSecuritySettingsBinder();

        public override ConnectionSettings Bind(ConfigurationNode node)
        {
            if (node is null || node.IsEmpty)
            {
                return ConnectionSettings.Default;
            }

            return new ConnectionSettings(
                node.GetValueAsGuid(nameof(ConnectionSettings.AccountId), ConnectionSettings.Default.AccountId),
                node.GetValueAsString(nameof(ConnectionSettings.Host), ConnectionSettings.Default.Host),
                node.GetValueAsInt32(nameof(ConnectionSettings.Port), ConnectionSettings.Default.Port),
                node.GetValueAsBoolean(nameof(ConnectionSettings.UseHostFromAccountAddress), ConnectionSettings.Default.UseHostFromAccountAddress),
                node.GetValueAsTimeSpan(nameof(ConnectionSettings.Timeout), ConnectionSettings.Default.Timeout),
                retryPolicyBinder.Bind(node.GetNodeOrEmpty(ConnectionRetryPolicyBinder.ConfigurationKey)),
                proxySettingsBinder.Bind(node.GetNodeOrEmpty(ConnectionProxySettingsBinder.ConfigurationKey)),
                securitySettingsBinder.Bind(node.GetNodeOrEmpty(ConnectionSecuritySettingsBinder.ConfigurationKey)));
        }

        public override ConfigurationNode Bind(ConnectionSettings settings)
        {
            if (settings is null || settings == ConnectionSettings.Default)
            {
                return ConfigurationNode.Empty;
            }

            return new ConfigurationNodeBuilder(ConfigurationKey)
                .AddProperty(nameof(ConnectionSettings.AccountId), settings.AccountId, ConnectionSettings.Default.AccountId)
                .AddProperty(nameof(ConnectionSettings.Host), settings.Host, ConnectionSettings.Default.Host, StringComparer.OrdinalIgnoreCase)
                .AddProperty(nameof(ConnectionSettings.Port), settings.Port, ConnectionSettings.Default.Port)
                .AddProperty(nameof(ConnectionSettings.UseHostFromAccountAddress), settings.UseHostFromAccountAddress, ConnectionSettings.Default.UseHostFromAccountAddress)
                .AddProperty(nameof(ConnectionSettings.Timeout), settings.Timeout, ConnectionSettings.Default.Timeout)
                .AddNode(retryPolicyBinder.Bind(settings.RetryPolicy))
                .AddNode(proxySettingsBinder.Bind(settings.Proxy))
                .AddNode(securitySettingsBinder.Bind(settings.Security))
                .Build();
        }
    }
}