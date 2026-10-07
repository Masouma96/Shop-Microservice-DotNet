using IDP.Application.Query.Auth;
using MediatR;
using System;
using Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IDP.Application.Handler.Query

{
    public class AuthHandler : IRequestHandler<AuthQuery, JsonWebToken>
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IOtpRedisRepository _redisRepository;
        private readonly IUserQueryRepository _userQueryRepository;


        public AuthHandler(IJwtHandler jwtHandler, IOtpRedisRepository otpRedisRepository, IUserQueryRepository userQueryRepository)
        {
            _jwtHandler = jwtHandler;
            _redisRepository = otpRedisRepository;
            _userQueryRepository = userQueryRepository;
        }
        public async Task<JsonWebToken> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _redisRepository.Getdata(request.MobileNumber);
                if (res == null) return null;

                if (res.OtpCode == request.OtpCode)
                {
                    var user = await _userQueryRepository.GetUserAsync(request.MobileNumber);
                    var token = _jwtHandler.Create(user.ID);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

    
}
