namespace Echo.Core.User
{
    public sealed record CardAddress(string Country, string Locality, string Region, string Street, string Exended, string PostalCode, CardAddressKind Kind);
}