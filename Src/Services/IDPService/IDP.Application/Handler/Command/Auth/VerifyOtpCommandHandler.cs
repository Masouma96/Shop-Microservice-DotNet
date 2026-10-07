using IDP.Application.Commands.Auth;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Handler.Command.Auth
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;

        public VerifyOtpCommandHandler(IOtpRedisRepository otpRedisRepository)
        {
            _otpRedisRepository = otpRedisRepository;
        }

        public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            // 1. Pull active database record using the user's ID
            var savedOtp = await _otpRedisRepository.Getdata(request.UserId.ToString());

            if (savedOtp == null)
                return false; // Code expired or never issued

            // 2. Perform validation logic against input values
            if (savedOtp.OtpCode.ToString() == request.InputCode)
            {
                // 3. Clear data out of cache immediately to prevent replays
                await _otpRedisRepository.Delete(savedOtp);
                return true;
            }

            return false; // Code mismatch

        }
    }
}
