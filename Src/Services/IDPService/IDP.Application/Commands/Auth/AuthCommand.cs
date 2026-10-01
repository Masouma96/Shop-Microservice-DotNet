using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Commands.Auth
{
    public  class AuthCommand:IRequest<bool>

    {
        public  required string MobileNumber { get; set; }
    }
}
