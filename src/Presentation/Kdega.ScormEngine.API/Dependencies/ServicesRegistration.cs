﻿using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Services;
using Kdega.ScormEngine.Infrastructure;

namespace Kdega.ScormEngine.API.Dependencies;
public static class ServicesRegistration
{
    public static string CorePolicy = "CorePolicy";
    public static void AddDependencyServices(this WebApplicationBuilder builder)
    {

        var services = builder.Services;

        services.AddRazorPages();
        services.AddHttpContextAccessor();
        services.AddHealthChecks();
        services.AddSwagger();
        services.AddSingleton<CurrentUserService>();
        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(CorePolicy, cfx => cfx.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        builder.AddDatabaseRegistration();


    }
}
