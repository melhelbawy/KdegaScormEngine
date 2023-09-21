using Microsoft.Extensions.DependencyInjection;

namespace Kdega.ScormEngine.Application.Extensions;
public static class ServiceProviderExtensions
{
    public static T GetCustomRequiredService<T>(this IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        return (T)serviceProvider.GetRequiredService(typeof(T));
    }

    private static object GetRequiredService(this IServiceProvider provider, Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(serviceType);

        if (provider is ISupportRequiredService requiredServiceSupportingProvider)
        {
            return requiredServiceSupportingProvider.GetRequiredService(serviceType);
        }

        object? service = provider.GetService(serviceType);
        if (service == null)
            throw new InvalidOperationException($"Service of type {serviceType.Name} is not registered.");

        return service;
    }
}
