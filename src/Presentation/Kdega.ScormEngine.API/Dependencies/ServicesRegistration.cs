using Kdega.ScormEngine.Application.Services;

namespace Kdega.ScormEngine.API.Dependencies;
public static class ServicesRegistration
{
    public static void AddDependencyServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddHttpContextAccessor();
        services.AddSwagger();
        services.AddSingleton<CurrentUserService>();


        builder.AddDatabaseRegistration();
    }
}
