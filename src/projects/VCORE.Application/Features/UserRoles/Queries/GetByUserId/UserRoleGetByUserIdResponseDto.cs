namespace VCORE.Application.Features.UserRoles.Queries.GetByUserId;

public class UserRoleGetByUserIdResponseDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public ICollection<string>? Roles { get; set; }
}