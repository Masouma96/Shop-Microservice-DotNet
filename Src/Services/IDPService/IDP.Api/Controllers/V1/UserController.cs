using IDP.Api.Controllers.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDP.Application.Commands.User;
using Asp.Versioning;

namespace IDP.Api.Controllers.V1
{
    
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/Users")]
    public class UserController : IBaseController
    {
        //تزریق وابستگی 
        public readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [MapToApiVersion(1)]

        [HttpPost("Insert")]

        public async Task<IActionResult> Insert([FromBody] UserCommands userCommand)
        {

            var res = await _mediator.Send(userCommand);

            return Ok(res);
        }

    }
}
