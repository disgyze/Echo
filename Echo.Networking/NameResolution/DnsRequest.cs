namespace Echo.Networking.NameResolution
{
    public abstract class DnsRequest
    {
        public abstract byte[] ToByteArray();
    }
}