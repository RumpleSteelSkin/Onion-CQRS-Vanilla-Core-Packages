namespace VCORE.Application.Features.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserResponseDto
{
    public string? Id { get; set; }
    public ICollection<string>? Roles { get; set; }
}