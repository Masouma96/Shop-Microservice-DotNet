using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Query.Auth
{
    public  record AuthQuery:IRequest<bool>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
