namespace Kdega.ScormEngine.API.Dependencies;
public static class ServicesRegistration
{
    public static void AddDependencyServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.AddDatabaseRegistration();
    }
}
