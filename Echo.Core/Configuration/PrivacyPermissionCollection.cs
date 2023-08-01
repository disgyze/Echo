namespace Echo.Core.Configuration
{
    public sealed record PrivacyPermissionCollection(PrivacyPermissionTarget MessageRead, PrivacyPermissionTarget MessageDelivery)
    {
        public static readonly PrivacyPermissionCollection Default = new PrivacyPermissionCollection(
            MessageRead: new PrivacyPermissionTarget(false, false, false),
            MessageDelivery: new PrivacyPermissionTarget(false, false, false));
    }
}