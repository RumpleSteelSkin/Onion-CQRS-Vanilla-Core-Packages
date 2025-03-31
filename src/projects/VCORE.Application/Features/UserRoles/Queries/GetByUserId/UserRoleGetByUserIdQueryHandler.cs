using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.UserRoles.Queries.GetByUserId;

public class UserRoleGetByUserIdQueryHandler(UserManager<User> userManager)
    : IRequestHandler<UserRoleGetByUserIdQuery, UserRoleGetByUserIdResponseDto>
{
    public async Task<UserRoleGetByUserIdResponseDto> Handle(UserRoleGetByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            throw new NotFoundException($"User with ID {request.UserId} not found!");
        
        return new UserRoleGetByUserIdResponseDto()
        {
            Email = user.Email,
            Roles = await userManager.GetRolesAsync(user),
            UserName = user.UserName
        };
    }
}