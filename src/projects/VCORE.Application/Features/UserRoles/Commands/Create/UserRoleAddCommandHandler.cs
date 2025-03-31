using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.UserRoles.Commands.Create;

public class UserRoleAddCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    : IRequestHandler<UserRoleAddCommand, string>
{
    public async Task<string> Handle(UserRoleAddCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            throw new NotFoundException("User not found!");
        IdentityRole<Guid>? role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null || string.IsNullOrWhiteSpace(role.Name))
            throw new NotFoundException("Role not found or has no valid name.");
        if ((await userManager.GetRolesAsync(user)).Any(x => x == role.Name))
            throw new BusinessException("User role already exist");
        IdentityResult addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
        if (!addRoleResult.Succeeded)
            throw new AuthorizationException(addRoleResult.Errors.Select(x => x.Description).ToList());
        return "User role added";
    }
}