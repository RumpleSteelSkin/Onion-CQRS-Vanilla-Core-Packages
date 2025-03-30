// using Core.CrossCuttingConcerns.Loggers.Serilog.ServiceBase;
// using MediatR;
//
// namespace Core.Application.Pipelines.Logging;
//
// public interface ILoggableRequest
// {
//     
// }
//
// public class LoggingPipeline<TRequest,TResponse>(LoggerService logger):IPipelineBehavior<TRequest,TResponse>
// where TRequest:IRequest<TResponse>,ILoggableRequest
// {
//     public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }
// }