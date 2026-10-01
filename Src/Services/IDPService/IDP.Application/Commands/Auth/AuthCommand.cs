
using MediatR;
using IDP.Application.DTO;
using IDP.Domain.IRepository.Command;

namespace IDP.Application.Commands.Auth;

public sealed record AuthCommand(string MobileNumber) : IRequest<bool>;

public sealed record AuthCommandHandler(IOtpRedisRepository<Otp> OtpRedisRepository) : IRequestHandler<AuthCommand, bool>
{
    public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        => await OtpRedisRepository.Insert(new Otp(231, "4341", false));
}