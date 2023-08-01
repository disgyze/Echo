using Echo.Configuration;

namespace Echo.Core.Configuration
{
    public sealed class ConnectionRetryPolicyBinder : ConfigurationBinder<ConnectionRetryPolicy>
    {
        public static readonly string ConfigurationKey = "RetryPolicy";

        public override ConnectionRetryPolicy Bind(ConfigurationNode node)
        {
            if (node is null || node.IsEmpty)
            {
                return ConnectionRetryPolicy.Default;
            }

            return new ConnectionRetryPolicy(
                node.GetValueAsBoolean(nameof(ConnectionRetryPolicy.IsEnabled), ConnectionRetryPolicy.Default.IsEnabled),
                node.GetValueAsInt32(nameof(ConnectionRetryPolicy.MaxAttempts), ConnectionRetryPolicy.Default.MaxAttempts),
                node.GetValueAsTimeSpan(nameof(ConnectionRetryPolicy.DelayBetweenAttempts), ConnectionRetryPolicy.Default.DelayBetweenAttempts));
        }

        public override ConfigurationNode Bind(ConnectionRetryPolicy settings)
        {
            if (settings is null || settings == ConnectionRetryPolicy.Default)
            {
                return ConfigurationNode.Empty;
            }

            return new ConfigurationNodeBuilder(ConfigurationKey)
                .AddProperty(nameof(ConnectionRetryPolicy.IsEnabled), settings.IsEnabled, ConnectionRetryPolicy.Default.IsEnabled)
                .AddProperty(nameof(ConnectionRetryPolicy.MaxAttempts), settings.MaxAttempts, ConnectionRetryPolicy.Default.MaxAttempts)
                .AddProperty(nameof(ConnectionRetryPolicy.DelayBetweenAttempts), settings.DelayBetweenAttempts, ConnectionRetryPolicy.Default.DelayBetweenAttempts)
                .Build();
        }
    }
}