using MediatR;

namespace VCORE.Application.Features.UserRoles.Queries.GetByUserId;

public class UserRoleGetByUserIdQuery : IRequest<UserRoleGetByUserIdResponseDto>
{
    public Guid UserId { get; set; }
}