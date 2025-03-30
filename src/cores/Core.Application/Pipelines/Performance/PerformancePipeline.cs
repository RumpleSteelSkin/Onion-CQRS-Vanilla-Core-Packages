using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Performance;

public class PerformancePipeline<TRequest, TResponse>(ILogger<PerformancePipeline<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    // , IPerformanceRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        TResponse response = await next();
        stopwatch.Stop();
        if(stopwatch.ElapsedMilliseconds > 0)
            logger.LogWarning($"{request.GetType()} : {stopwatch.ElapsedMilliseconds} MS");
        return response;
    }
}