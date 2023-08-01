namespace Echo.Core.Configuration
{
    public sealed record PrivacySettings(bool IsMutingMicrophoneOnCtrlSpaceEnabled, PrivacyPermissionCollection Permissions)
    {
        public static readonly PrivacySettings Default = new PrivacySettings(IsMutingMicrophoneOnCtrlSpaceEnabled: true, Permissions: PrivacyPermissionCollection.Default);
    }
}