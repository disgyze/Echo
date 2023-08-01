namespace Echo.Core.UI
{
    public interface IDisplay
    {
        void Clear();
        void ShowEcho(string text);
        void ShowOther(string text);
        void ShowError(string text);
        void ShowSeparator(string? text = null);
        void ShowIncomingXml(string text);
        void ShowOutgoingXml(string text);
        void ShowConnectionInfo(string text);
        void ShowConnectionError(string text);
        void ShowAuthenticationInfo(string? text);
        void ShowAuthenticationError(string text);
        void ShowCertificateInfo(string text);
        void ShowCertificateError(string text);
        void ShowCertificateWarning(string text);
        void ShowChatAction(XmppAddress sender, XmppAddress target, string text);
        void ShowChatMessage(XmppAddress sender, XmppAddress target, string text);
        void ShowChannelJoin(XmppAddress sender, bool isMe = false);
        void ShowChannelLeave(XmppAddress sender, string? reason = null, bool isMe = false);
        void ShowChannelInvite(XmppAddress sender, XmppAddress channel, string? reason = null);
        void ShowChannelAction(XmppAddress channel, XmppAddress sender, string text);
        void ShowChannelMessage(XmppAddress channel, XmppAddress sender, string text);
        void ShowArchiveChatMessage(XmppAddress sender, XmppAddress target, string text);
        void ShowArchiveChannelMessage(XmppAddress channel, XmppAddress sender, string text);
        void EditChatMessage(XmppAddress target, string messageId, string text);
        void EditChannelMessage(XmppAddress target, string messageId, string text);
        void RetractChatMessage(XmppAddress target, string messageId, string? fallbackText = null);
        void RetractChannelMessage(XmppAddress target, string messageId, string? fallbackText = null);
        void MarkAsReadChatMessage(XmppAddress target, string messageId);
        void MarkAsDeliveredChatMessage(XmppAddress target, string messageId);
    }
}