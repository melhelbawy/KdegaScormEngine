using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Services;
using Kdega.ScormEngine.Infrastructure;

namespace Kdega.ScormEngine.API.Dependencies;
public static class ServicesRegistration
{
    public static void AddDependencyServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddHttpContextAccessor();
        services.AddSwagger();
        services.AddSingleton<CurrentUserService>();
        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);

        builder.AddDatabaseRegistration();
    }
}
