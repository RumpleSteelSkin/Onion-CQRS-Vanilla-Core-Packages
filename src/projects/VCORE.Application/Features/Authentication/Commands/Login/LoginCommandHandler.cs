using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VCORE.Application.Services.JwtServices;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler(UserManager<User> userManager, IJwtService jwtService)
    : IRequestHandler<LoginCommand, AccessTokenDto>
{
    public async Task<AccessTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request.Email is null)
            throw new NotFoundException("Email cannot be null.");

        User? emailUser = await userManager.FindByEmailAsync(request.Email);

        if (emailUser is null)
            throw new NotFoundException("No user found with the specified email address.");

        if (request.Password is null)
            throw new NotFoundException("Password cannot be null.");

        if (await userManager.CheckPasswordAsync(emailUser, request.Password) is false)
            throw new BusinessException("Invalid password. Please try again.");
        
        return await jwtService.CreateTokenAsync(emailUser);
    }
}