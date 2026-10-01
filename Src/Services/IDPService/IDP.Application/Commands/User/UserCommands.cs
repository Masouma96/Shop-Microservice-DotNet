
using MediatR;

namespace IDP.Application.Commands.User;

public sealed record UserCommands(string FullName, string CodeNumber) : IRequest<bool>;

public sealed record UserHandler : IRequestHandler<UserCommands, bool>
{
    public async Task<bool> Handle(UserCommands request, CancellationToken cancellationToken)
        => true;
}