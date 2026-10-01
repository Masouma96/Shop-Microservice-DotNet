

namespace IDP.Application.DTO;

public sealed record Otp(long UserId, string OtpCode, bool IsUsed);
