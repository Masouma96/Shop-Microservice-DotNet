using Asp.Versioning;
using AutoMapper;
using IDP.Application.Handler.Command.User;
using IDP.Application.Helper;
using IDP.Domain.DTO;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Command.Base;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command;
using IDP.Infra.Repository.Command.Base;
using IDP.Infra.Repository.Query;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ۱. تنظیمات متمرکز و واحد داکر ردیس روی پورت ویندوز (۶۳۸۰)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:RedisUrl") ?? "127.0.0.1:6380";
});

// ۲. تنظیم اتوماتیک AutoMapper برای اسکن لایه Application
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ۳. تزریق وابستگی‌های دیتابیس و ریپازیتوری‌ها (تایید لایه‌ها)
builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

builder.Services.AddTransient<ShopCommandDbContext>();
builder.Services.AddTransient<ShopQueryDbContext>();

// ۴. تنظیمات مدیاتور
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));

// ۵. تنظیمات نسخه‌بندی API (Versioning)
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
builder.Services.AddMassTransit(busConfig =>
{


    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
        {
            
            h.Username(builder.Configuration.GetValue<string>("Rabbit:UserName"));

            h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));

        });

        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

        cfg.ConfigureEndpoints(context);
    });
});



// ۶. فعال‌سازی ماژول احراز هویت توکن مشترک (کارت هوشمند دیجیتال)
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
