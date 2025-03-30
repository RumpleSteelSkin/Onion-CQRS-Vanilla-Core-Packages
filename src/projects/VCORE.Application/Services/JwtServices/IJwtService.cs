using VCORE.Domain.Models;

namespace VCORE.Application.Services.JwtServices;

public interface IJwtService
{
    Task<AccessTokenDto> CreateTokenAsync(User user);
}