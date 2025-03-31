using MediatR;
using Microsoft.EntityFrameworkCore;

namespace VCORE.Application.Features.UserRoles.Commands.Create;

public class UserRoleAddCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}