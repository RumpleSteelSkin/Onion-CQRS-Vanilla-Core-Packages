using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace VCORE.Application.Features.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<GetCurrentUserResponseDto>
{
    public ClaimsPrincipal? CurrentUser { get; set; }
    public HttpContext? CurrentHttpContext { get; set; }
}