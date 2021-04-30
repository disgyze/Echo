namespace Echo.Networking.Dns
{
    public enum DnsOperation : byte
    {
        Query = 0,
        Status = 2,
        Notify = 4,
        Update = 5
    }
}