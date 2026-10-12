using AutoMapper;
using DotNetCore.CAP;
using EventBus.Events;

using IDP.Application.Commands.Auth;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDP.Application.Handler.Command.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {

        private readonly IOtpRedisRepository _otpRedisRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;
        //private readonly ICapPublisher _capBus;
        private readonly IPublishEndpoint _publishEndpoint;

    

        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository,
             IUserCommandRepository userCommandRepository,
             IPublishEndpoint publishEndpoint,
             IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _otpRedisRepository = otpRedisRepository;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            //_capBus = capPublisher;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userobj = _mapper.Map<IDP.Domain.Entities.User>(request);
                var user = await _userQueryRepository.GetUserAsync(request.MobileNumber);
                if (user == null)
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                

                    userobj.UserName = request.MobileNumber;
                    var res = await _userCommandRepository.Insert(userobj);
             
                    await _otpRedisRepository.Insert(new Domain.DTO.Otp { UserName = userobj.MobileNumber, OtpCode = code, IsUse = false });
                    //ارسال پیامک به سرویس نوتفیکیشن
                    //await _capBus.PublishAsync<AuthCommand>("otpevent", new AuthCommand
                    //{
                    //    MobileNumber = request.MobileNumber,
                    //});
                    await _publishEndpoint.Publish<OtpEvents>(new OtpEvents
                    {

                        MobileNumber = request.MobileNumber,
                        OtpCode = code.ToString(),
                    }, __ => { }, cancellationToken);
                }

                else
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                   

                    userobj.UserName = request.MobileNumber;
                    await _otpRedisRepository.Insert(new Domain.DTO.Otp { UserName = user.MobileNumber, OtpCode = code, IsUse = false });
                    //ارسال پیامک به سزویس نوتفیکیشن
                    //await _capBus.PublishAsync<AuthCommand>("otpevent", new AuthCommand
                    //{
                    //    MobileNumber = request.MobileNumber,
                    //});
                    await _publishEndpoint.Publish<OtpEvents>(new OtpEvents
                    {

                        MobileNumber = request.MobileNumber,
                        OtpCode = code.ToString(),
                    });
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}