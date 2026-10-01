using Auth;
using IDP.Application.Query.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Handler.Query
{
    public class AuthHandler : IRequestHandler<AuthQuery, bool>
    {
        private readonly IJwtHandler _jwtHandler;
        public AuthHandler(IJwtHandler jwtHandler) 
        {
            _jwtHandler = jwtHandler;

        }
        public async Task<bool> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
           var token =   _jwtHandler.Create(34);
            return  true;
        }
    }
}
