using System;
using System.Collections.Immutable;

namespace Echo.Core.User
{
    public sealed record Card(string? Nick,
                              CardFullName? FullName,
                              DateTimeOffset Birthday,
                              string? Description,
                              ImmutableArray<CardPhone> PhoneNumbers,
                              ImmutableArray<CardAddress> Addresses);
}