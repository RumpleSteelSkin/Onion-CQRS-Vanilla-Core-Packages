using System.Text.Json;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Microsoft.AspNetCore.Diagnostics;

namespace VCORE.Presentation.Middlewares;

public class HttpExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        switch (exception)
        {
            case NotFoundException notFound:
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new NotFoundProblemDetails(notFound.Message)),
                    cancellationToken: cancellationToken);
                return false;
            }
            case BusinessException business:
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new BusinessProblemDetails(business.Message)),
                    cancellationToken: cancellationToken);
                return false;
            }
            case AuthorizationException authorization:
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new AuthorizationProblemDetails(authorization.Errors)),
                    cancellationToken: cancellationToken);
                return false;
            }
            case FluentValidationException fluentValidation:
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new ValidationProblemDetails(fluentValidation.Errors)),
                    cancellationToken: cancellationToken);
                return false;
            }
            default:
                return true;
        }
    }
}