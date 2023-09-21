using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Kdega.ScormEngine.Application.Behavior.Logging;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull

{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {0}", typeof(TRequest).Name);

        var type = request?.GetType();

        var props = new List<PropertyInfo>(type!.GetProperties());

        foreach (var prop in props)
        {
            var value = prop.TreatSensitivity(request!);

            _logger.LogInformation("{Property} : {@Value}", prop.Name, value);
        }

        var response = await next();

        // Logging response
        _logger.LogInformation("Handled {0} response of {1}", typeof(TResponse).Name, typeof(TRequest).Name);

        return response;
    }
}
