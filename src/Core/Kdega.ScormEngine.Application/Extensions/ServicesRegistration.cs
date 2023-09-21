using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Application.Behavior.ExceptionBehavior;
using Kdega.ScormEngine.Application.Behavior.Logging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kdega.ScormEngine.Application.Extensions;
public static class ServicesRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly));
    }
}
