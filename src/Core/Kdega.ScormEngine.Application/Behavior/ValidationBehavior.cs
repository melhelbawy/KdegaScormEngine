using FluentValidation;
using Kdega.ScormEngine.Application.Behavior.Models;
using MediatR;
using Serilog;

namespace Kdega.ScormEngine.Application.Behavior;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (!failures.Any()) return await next();

        var errors = failures
            .Select(e => new ErrorRecord(e.PropertyName, e.ErrorCode, e.ErrorMessage))
            .ToList();

        errors.ForEach(x =>
        {
            _logger.Error($"Validation Error: in {x.PropertyName}, Message: {x.Message}, type: {x.Type}");
        });

        throw new ExceptionBehavior.ValidationException(errors);
    }
}
