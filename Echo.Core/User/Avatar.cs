namespace Echo.Core.User
{
    public sealed record Avatar(AvatarSource Source, int Width, int Height, long SizeInBytes, string MediaType);
}