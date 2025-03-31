using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using FluentValidation;
using MediatR;

namespace Core.Application.Pipelines.Validation;

public class ValidationPipeline<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        IEnumerable<ValidationExceptionModel> errors = validators
            .Select(validator => validator.Validate(new ValidationContext<object>(request)))
            .SelectMany(result => result.Errors)
            .GroupBy(
                p => p.PropertyName,
                (propName, errors) =>
                    new ValidationExceptionModel()
                    {
                        Property = propName,
                        Errors = errors.Select(x => x.ErrorMessage)
                    }
            ).ToList();
        if (errors.Any())
            throw new FluentValidationException(errors);
        return await next();
    }
}