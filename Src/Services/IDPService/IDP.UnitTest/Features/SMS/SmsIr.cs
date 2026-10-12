using Carter;
using EventBus.Events;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using VerticalNotification.Contracts;

namespace VerticalNotification.Features.SMS
{
    public static class SmsIr
    {
  
        public class Command : IRequest<bool>
        {
            public string Msg { get; set; } = string.Empty;
            public string MobileNumber { get; set; } = string.Empty;
            public string OtpCode { get; set; } = string.Empty;
        }

   
        public sealed class Handler : IRequestHandler<Command, bool>
        {
            private readonly IConfiguration _configuration;
            private readonly IHttpClientFactory _httpClientFactory;

            public Handler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            {
                _configuration = configuration;
                _httpClientFactory = httpClientFactory;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

  
    public class SmsIrEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("/api/v1/sendsmsIr", async (SmsRequest request , ISender sender) =>
            {
                var command = request.Adapt<SmsIr.Command>();

                var result = await sender.Send(command);
                return  Results.Ok(true);
            })
            .WithName("sendsmsIr")
            .WithTags("sendsmsIr");
        }
    }

  
    public class GetEvent : IConsumer<OtpEvents>
    {
        private readonly ISender _mediator;

   
        public GetEvent(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OtpEvents> context)
        {
            Console.WriteLine($"mobile: {context.Message.Id}");


            await _mediator.Send(new SmsIr.Command
            {
                MobileNumber = context.Message.MobileNumber,

                Msg = $"Your OTP code is: {context.Message.OtpCode}"
            });
        }
    }
}
