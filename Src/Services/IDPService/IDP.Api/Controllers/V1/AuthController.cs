
using Auth;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using IDP.Application.Query.Auth;
using IDP.Application.Commands.Auth;

namespace IDP.Api.Controllers.V1;

[ApiController, Route("api/v{v:apiVersion}/[controller]"), ApiVersion(1), ApiVersion(2)]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost(nameof(Login))]
    public async Task<JsonWebToken> Login([FromBody] AuthQuery authQuery, CancellationToken cancellationToken)
        => await mediator.Send(authQuery, cancellationToken);

    [HttpPost(nameof(RegisterAndSendOtp))]
    public async Task<bool> RegisterAndSendOtp([FromBody] AuthCommand authCommand, CancellationToken cancellationToken)
        => await mediator.Send(authCommand, cancellationToken);
}