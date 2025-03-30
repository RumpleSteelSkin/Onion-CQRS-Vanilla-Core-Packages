using System.Security.Claims;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationPipeline<TRequest, TResponse>(IHttpContextAccessor accessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IRoleExists
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        HttpContext httpContext = accessor.HttpContext;
        if (httpContext.User.Identity is { IsAuthenticated: false })
            throw new AuthorizationException("Not Login.");

        IList<Claim> userClaims = httpContext.User.Claims.ToList();
        if (userClaims == null || userClaims.Count == 0)
            throw new AuthorizationException("You don't have authority.");

        List<string> roles = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
            .ToList();

        if (request.Roles.Any(role => roles.Contains(role)) is false)
            throw new AuthorizationException("You don't have authority.");

        return await next();
    }
}