using Asp.Versioning;
using IDP.Application.Query.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDP.Application.Commands.Auth;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/Auth")]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
         
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);
            return Ok(res);

        }
   
        /// ثبت نام و ارسال کد به شماره موبایل
 
        [HttpPost("RegisterAndSendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authCommand)
        {
            var res = await _mediator.Send(authCommand);

            return Ok(res);
        }

    }
}
