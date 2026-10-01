using IDP.Application.Commands.User;
using MediatR;

namespace IDP.Application.Handler.Command.User
{
    public class UserHandler : IRequestHandler<UserCommands, bool>
    {

        public async Task<bool> Handle(UserCommands request, CancellationToken cancellationToken)
        {

            return true;
        }
    }
}
