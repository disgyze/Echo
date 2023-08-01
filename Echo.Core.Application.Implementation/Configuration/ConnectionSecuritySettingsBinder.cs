using Echo.Configuration;

namespace Echo.Core.Configuration
{
    public sealed class ConnectionSecuritySettingsBinder : ConfigurationBinder<ConnectionSecuritySettings>
    {
        public static readonly string ConfigurationKey = "Security";

        public override ConnectionSecuritySettings Bind(ConfigurationNode node)
        {
            if (node is null || node.IsEmpty)
            {
                return ConnectionSecuritySettings.Default;
            }

            return new ConnectionSecuritySettings(
                node.GetValueAsEnum(nameof(ConnectionSecuritySettings.EncryptionRequirement), ConnectionSecuritySettings.Default.EncryptionRequirement),
                node.GetValueAsEnum(nameof(ConnectionSecuritySettings.CertificateValidationErrorAction), ConnectionSecuritySettings.Default.CertificateValidationErrorAction));
        }

        public override ConfigurationNode Bind(ConnectionSecuritySettings settings)
        {
            if (settings is null || settings == ConnectionSecuritySettings.Default)
            {
                return ConfigurationNode.Empty;
            }

            return new ConfigurationNodeBuilder(ConfigurationKey)
                .AddProperty(nameof(ConnectionSecuritySettings.EncryptionRequirement), settings.EncryptionRequirement, ConnectionSecuritySettings.Default.EncryptionRequirement)
                .AddProperty(nameof(ConnectionSecuritySettings.CertificateValidationErrorAction), settings.CertificateValidationErrorAction, ConnectionSecuritySettings.Default.CertificateValidationErrorAction)
                .Build();
        }
    }
}