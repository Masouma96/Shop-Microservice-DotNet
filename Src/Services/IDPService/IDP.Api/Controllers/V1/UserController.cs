
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using IDP.Application.Commands.User;

namespace IDP.Api.Controllers.V1;

[ApiController, Route("api/v{v:apiVersion}/[controller]"), ApiVersion(1), ApiVersion(2)]
public class UserController(IMediator mediator)
{
    [HttpPost(nameof(Insert)), MapToApiVersion(1)]
    public async Task<bool> Insert([FromBody] UserCommands userCommand, CancellationToken cancellation)
        => await mediator.Send(userCommand, cancellation);
}
