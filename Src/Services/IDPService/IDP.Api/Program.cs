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
using VerticalNotification.Features.SMS;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:RedisUrl") ?? "127.0.0.1:6380";
});
builder.Services.AddHttpClient();


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

builder.Services.AddTransient<ShopCommandDbContext>();
builder.Services.AddTransient<ShopQueryDbContext>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(VerticalNotification.Features.SMS.SmsIr.Handler).Assembly));


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

//builder.Services.AddCap(options =>
//{
//    options.UseEntityFramework<ShopCommandDbContext>();
//    options.UseDashboard(path => path.PathMatch = "/cap");
//    options.UseRabbitMQ(options =>
//    {
//        options.ConnectionFactoryOptions = options =>
//        {
//            options.Ssl.Enabled = false;
//            options.HostName = "localhost";
//            options.UserName = "guest";
//            options.Password = "guest";
//            options.Port = 5672;
//        };

//    });
//    options.FailedRetryCount = 5;
//    options.FailedRetryInterval = 60;

//});
//builder.Services.AddSingleton<CustomerAddedEventSubscriber>();

builder.Services.AddMassTransit(busConfig =>
{


    //busConfig.AddConsumer<GetEvent>();
    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
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
