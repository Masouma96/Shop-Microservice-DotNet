using IDP.Application.Commands.Auth;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Handler.Command.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {

        private readonly IOtpRedisRepository _otpRedisRepository;
        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository)
        {
            _otpRedisRepository = otpRedisRepository; 

        }
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            await _otpRedisRepository.Insert(new Domain.DTO.Otp { UserId = 231, OtpCode = "4341", IsUsed = false });
            return true;
        }
    }
}
  