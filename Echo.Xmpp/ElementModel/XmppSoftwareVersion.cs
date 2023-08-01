namespace Echo.Xmpp.ElementModel
{
    public class XmppSoftwareVersion : XmppSoftwareVersionQuery
	{
		public string? SoftwareName
		{
			get => (string?)Element(Name.Namespace + "name");
            set => SetElementValue(Name.Namespace + "name", value);
        }

		public string? SoftwareVersion
		{
			get => (string?)Element(Name.Namespace + "version");
			set => SetElementValue(Name.Namespace + "version", value);
		}

		public string? OperatingSystem
		{
			get => (string?)Element(Name.Namespace + "os");
			set => SetElementValue(Name.Namespace + "os", value);
		}

		public XmppSoftwareVersion(string softwareName, string softwareVersion, string operatingSystem) : base()
		{
			SoftwareName = softwareName;
			SoftwareVersion = softwareVersion;
			OperatingSystem = operatingSystem;
		}
	}
}