namespace Echo.Core.Configuration
{
    public sealed record DisplayMessageComposeSettings(bool IsSpellCheckingEnabled,
                                                       char CommandSymbol,
                                                       char MentionSymbol,
                                                       DisplayMessageComposeSendShortcut SendShortcut,
                                                       DisplayMessageComposeToolbarLayout ToolbarLayout);
}