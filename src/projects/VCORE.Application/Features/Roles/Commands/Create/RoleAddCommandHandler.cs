using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace VCORE.Application.Features.Roles.Commands.Create;

public class RoleAddCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
    : IRequestHandler<RoleAddCommand, string>
{
    public async Task<string> Handle(RoleAddCommand request, CancellationToken cancellationToken)
    {
        if (await roleManager.RoleExistsAsync(request.Name))
            throw new BusinessException("Role with the same name already exists.");
        var result = await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = request.Name });
        if (!result.Succeeded)
            throw new BusinessException($"An error occurred while creating the role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        return $"Role '{request.Name}' has been successfully created.";
    }
}