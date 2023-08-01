namespace Echo.Core.Configuration
{
    public sealed record ConnectionSecuritySettings(ConnectionEncryptionRequirement EncryptionRequirement, ConnectionCertificateValidationErrorAction CertificateValidationErrorAction)
    {
        public static readonly ConnectionSecuritySettings Default = new ConnectionSecuritySettings(
                EncryptionRequirement: ConnectionEncryptionRequirement.Required,
                CertificateValidationErrorAction: ConnectionCertificateValidationErrorAction.Decline);
    }
}