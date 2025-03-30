using MediatR;
using VCORE.Application.Services.JwtServices;

namespace VCORE.Application.Features.Authentication.Commands.Login;

public class LoginCommand : IRequest<AccessTokenDto>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}