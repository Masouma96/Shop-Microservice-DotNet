
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IDP.Infra;

public static class DependencyInjection
{
    public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShopCommandDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("CommandDBConnection"));
            options.EnableDetailedErrors();
        });
    }
}
