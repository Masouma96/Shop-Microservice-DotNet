using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace IDP.Application.Commands.Auth
{
    public  class VerifyOtpCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string InputCode { get; set; }
    }
}
