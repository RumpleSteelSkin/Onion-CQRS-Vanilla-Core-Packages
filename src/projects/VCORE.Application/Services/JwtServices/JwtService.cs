using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VCORE.Domain.Models;

namespace VCORE.Application.Services.JwtServices;

public class JwtService(UserManager<User> userManager, IOptions<CustomTokenOptions> options)
    : IJwtService
{
    private readonly CustomTokenOptions _customTokenOptions = options.Value;

    public async Task<AccessTokenDto> CreateTokenAsync(User user)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccessTokenExpiration);
        SigningCredentials signingCredentials = new(GetSecurityKey(_customTokenOptions.SecurityKey),
            SecurityAlgorithms.HmacSha512Signature);

        return new AccessTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _customTokenOptions.Issuer,
                audience: _customTokenOptions.Audience?[0],
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials,
                claims: await GetClaims(user)
            )),
            TokenExpiration = accessTokenExpiration
        };
    }


    private async Task<List<Claim>> GetClaims(User user)
    {
        List<Claim> claimList =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        ];
        IList<string> roles = await userManager.GetRolesAsync(user);
        if (roles.Count > 0)
            claimList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
        return claimList;
    }

    private static SecurityKey GetSecurityKey(string? key)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? string.Empty));
    }
}