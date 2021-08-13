namespace Echo.Xmpp.ElementModel
{
    public enum XmppSaslFailureReason
	{
		Unknown,
		Aborted,
		AccountDisabled,
		CredentialsExpired,
		EncryptionRequired,
		IncorrectEncoding,
		InvalidAuthzid,
		InvalidMechanism,
		MalformedRequest,
		MechanismTooWeak,
		NotAuthorized,
		TemporaryAuthFailure
	}
}