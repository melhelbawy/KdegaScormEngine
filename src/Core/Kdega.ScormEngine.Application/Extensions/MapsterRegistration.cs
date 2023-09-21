using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Kdega.ScormEngine.Application.Extensions;
public static class MapsterRegistration
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(typeof(ApplicationLayer).Assembly);
    }
}
