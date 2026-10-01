
namespace Auth;

public sealed record JsonWebToken(string? Token, long Expires, long RefreshToken);
