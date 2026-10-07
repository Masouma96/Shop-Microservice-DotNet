using Asp.Versioning;
using IDP.Application.Commands.Auth;

using IDP.Application.Query.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand verifyOtpCommand)
        {
            var res = await _mediator.Send(verifyOtpCommand);
            if (!res) return BadRequest("Invalid or expired code.");

            return Ok(new { Message = "Authentication Successful" });
        }

    }
}
