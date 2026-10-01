
using Auth;
using MediatR;

namespace IDP.Application.Query.Auth;

public sealed record AuthQuery(string? UserName, string? Password) : IRequest<JsonWebToken>;

public sealed record AuthHandler(IJwtHandler JwtHandler) : IRequestHandler<AuthQuery, JsonWebToken>
{
    public async Task<JsonWebToken> Handle(AuthQuery request, CancellationToken cancellationToken)
        => JwtHandler.Create(34);
}