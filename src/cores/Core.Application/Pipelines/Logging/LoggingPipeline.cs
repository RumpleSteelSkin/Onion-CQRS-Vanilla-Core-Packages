using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.CrossCuttingConcerns.Loggers.Models;
using Core.CrossCuttingConcerns.Loggers.Serilog.ServiceBase;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Logging;

public class LoggingPipeline<TRequest, TResponse>(LoggerService logger, IHttpContextAccessor contextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
// , ILoggableRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.Info(JsonSerializer.Serialize(new LogDetail
        {
            MethodName = typeof(TRequest).Name,
            Parameters = [new LogParameter { Type = request.GetType().Name, Value = request.ToString() }],
            User = contextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "Anonymous"
        }, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Default,
            WriteIndented = true,
            IgnoreReadOnlyProperties = false,
            IncludeFields = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        }));
        return await next();
    }
}