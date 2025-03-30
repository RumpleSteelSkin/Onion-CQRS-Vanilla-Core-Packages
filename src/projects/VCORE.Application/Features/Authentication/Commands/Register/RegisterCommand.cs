using MediatR;
using VCORE.Application.Services.JwtServices;

namespace VCORE.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : IRequest<AccessTokenDto>
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}