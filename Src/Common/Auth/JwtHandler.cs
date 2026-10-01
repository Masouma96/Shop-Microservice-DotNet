
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Auth;

public class JwtHandler : IJwtHandler
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly SecurityKey _issuerSigningKey;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtHeader _jwtHeader;
    private readonly IConfiguration _configuration;
    
    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"]!));
        _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
        _jwtHeader = new JwtHeader(_signingCredentials);
    }

    public JsonWebToken Create(long userId)
    {
        var nowUtc = DateTime.UtcNow;
        var expires = nowUtc.AddMinutes(int.Parse(_configuration["jwt:expiryMinutes"]!));
        var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
        var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalMilliseconds);
        var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalMilliseconds);

        var payload = new JwtPayload
        {
            {"sub",userId },
            {"iss", _configuration["jwt:issuer"]! },
            {"iat", now },
            {"exp",exp },
            {"unique_code",userId },
        };
        var jwt = new JwtSecurityToken(_jwtHeader, payload);
        var token = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JsonWebToken(token, exp, 0);
    }
}