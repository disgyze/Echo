namespace Echo.Core.Configuration
{
    public sealed record DesktopIntegrationSettings(bool IsXmppUriAssociationEnabled, bool IsTrayIconEnabled, bool IsRunOnSystemStartupEnabled, bool IsMinimizeOnSystemStartupEnabled)
    {
        public static readonly DesktopIntegrationSettings Default = new DesktopIntegrationSettings(
            IsXmppUriAssociationEnabled: false,
            IsTrayIconEnabled: true,
            IsRunOnSystemStartupEnabled: false,
            IsMinimizeOnSystemStartupEnabled: true);
    }
}