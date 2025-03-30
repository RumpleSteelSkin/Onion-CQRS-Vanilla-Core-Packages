using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VCORE.Application.Services.JwtServices;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler(UserManager<User> userManager, IJwtService jwtService)
    : IRequestHandler<RegisterCommand, AccessTokenDto>
{
    public async Task<AccessTokenDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BusinessException("Email is required.");
        if (string.IsNullOrWhiteSpace(request.Password))
            throw new BusinessException("Password is required.");
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new BusinessException("First name is required.");
        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new BusinessException("Last name is required.");
        if (string.IsNullOrWhiteSpace(request.UserName))
            throw new BusinessException("Username is required.");

        if (await userManager.FindByEmailAsync(request.Email) is not null)
            throw new BusinessException("A user with this email address already exists.");
    
        if (await userManager.FindByNameAsync(request.UserName) is not null)
            throw new BusinessException("A user with this username already exists.");

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
        };

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"User registration failed: {errorMessages}");
        }

        AccessTokenDto token = await jwtService.CreateTokenAsync(user);

        return token;
    }

}