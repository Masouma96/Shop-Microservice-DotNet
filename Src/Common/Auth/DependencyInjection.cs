
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth;

public static class DependencyInjection
{
	public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        => services
          .AddSingleton<IJwtHandler, JwtHandler>()
          .AddAuthentication()
          .AddJwtBearer(cfg =>
          {
              cfg.RequireHttpsMetadata = false;
              cfg.SaveToken = true;
              cfg.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateAudience = false,
                  ValidIssuer = configuration["jwt:issuer"]!,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"]!))
              };
          });
}