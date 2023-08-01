namespace Echo.Networking.NameResolution
{
    public enum DnsResponseResult : byte
    {
        OK = 0,
        FormatError = 1,
        ServerFailure = 2,
        NameError = 3,
        NotImplemented = 4,
        Refused = 5,
        NotAuth = 9,
        NotZone = 10
    }
}