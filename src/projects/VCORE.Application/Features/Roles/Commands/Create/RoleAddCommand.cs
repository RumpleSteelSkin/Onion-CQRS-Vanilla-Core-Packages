using MediatR;

namespace VCORE.Application.Features.Roles.Commands.Create;

public class RoleAddCommand : IRequest<string>
{
    public required string Name { get; set; }
}