namespace Echo.Xmpp.Client.Authentication
{
	public static class KnownMechanisms
	{
		public const string ScramSha1 = "SCRAM-SHA-1";
		public const string DigestMD5 = "DIGEST-MD5";
		public const string Plaintext = "PLAIN";
		public const string Anonymous = "ANONYMOUS";
		public const string External = "EXTERNAL";
		public const string Kerberos = "KERBEROS";
		public const string Facebook = "";
		public const string GoogleToken = "X-GOOGLE-TOKEN";
		public const string WindowsLiveMessenger = "X-MESSENGER-OAUTH2";
	}
}