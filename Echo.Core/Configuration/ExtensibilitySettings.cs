namespace Echo.Core.Configuration
{
    public sealed record ExtensibilitySettings(bool IsReloadOnChangeEnabled)
    {
        public static readonly ExtensibilitySettings Default = new ExtensibilitySettings(IsReloadOnChangeEnabled: false);
    }
}