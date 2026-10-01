
using Auth;
using MediatR;
using IDP.Infra;
using Asp.Versioning;
using IDP.Application.DTO; // لایه پایه کامند
using IDP.Infra.Repository.Command;
using IDP.Application.Commands.Auth;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Command.Base;

namespace IDP.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var environment = builder.Environment.EnvironmentName;
        builder.Configuration.AddJsonFile($"appsettings.{environment}.json", false, true);

        // ۱. تنظیمات ردیس را فقط یک‌بار و کاملاً درست تعریف می‌کنیم
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:RedisUrl");
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // ۲. اتصال اینترفیس اختصاصی و پایه به کلاس اجرایی ردیس
        builder.Services.AddScoped<IOtpRedisRepository<Otp>, OtpRedisRepository>();
        builder.Services.AddScoped<ICommandRepository<Otp>, OtpRedisRepository>();

        // تنظیمات مدیاتور
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AuthCommand).Assembly));

        // تنظیمات نسخه بندی API
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddJwt(builder.Configuration); // new
        builder.Services.AddInfrastructure(builder.Configuration); // new

        var app = builder.Build();

        if (!app.Environment.IsProduction())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        await app.RunAsync();
    }
}
