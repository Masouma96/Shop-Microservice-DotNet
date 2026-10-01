using Asp.Versioning;
using IDP.Application.Handler.Command.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Command.Base; // لایه پایه کامند
using IDP.Domain.DTO;
using IDP.Infra.Repository.Command;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
        builder.Services.AddScoped<ICommandRepository<Otp>, OtpRedisRepository>();

        // تنظیمات مدیاتور
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));

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

        Auth.Extensions.AddJwt(builder.Services, builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
