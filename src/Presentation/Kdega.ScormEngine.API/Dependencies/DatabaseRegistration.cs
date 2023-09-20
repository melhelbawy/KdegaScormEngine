using Kdega.ScormEngine.API.Extensions;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Application.Services;
using Kdega.ScormEngine.Persistence;
using Kdega.ScormEngine.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.API.Dependencies;
public static class DatabaseRegistration
{
    public static void AddDatabaseRegistration(this WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var env = serviceProvider.GetRequiredService<IHostEnvironment>();

        if (env.IsTesting())
            RegisterInMemoryDbContext(builder);
        else
            RegisterSqlDbContext(builder);
    }
    private static void RegisterSqlDbContext(WebApplicationBuilder builder)
    {
        #region DB Context
        var services = builder.Services;
        var serviceProvider = builder.Services.BuildServiceProvider();

        var sqlServerConfiguration = builder.Configuration.GetSection("SqlServerConfiguration")
            .Get<SqlServerConfiguration>() ?? new SqlServerConfiguration();
        var currentUserService = serviceProvider.GetRequiredService<CurrentUserService>();

        services.AddDbContext<IKseDbContext, KseDbContext>(b => b
            .UseSqlServer(sqlServerConfiguration.GetConnectionString())
            .AddInterceptors(new AuditableEntitySaveChangesInterceptor(currentUserService)));

        #endregion
    }

    private static void RegisterInMemoryDbContext(WebApplicationBuilder builder)
    {
        #region DB Context
        var services = builder.Services;
        var serviceProvider = builder.Services.BuildServiceProvider();
        var currentUserService = serviceProvider.GetRequiredService<CurrentUserService>();

        services.AddDbContext<IKseDbContext, KseDbContext>(b => b
            .UseInMemoryDatabase("KdegaScormPlayerDb")
            .AddInterceptors(new AuditableEntitySaveChangesInterceptor(currentUserService)));
        #endregion
    }
}
