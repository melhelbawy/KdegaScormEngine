using Kdega.ScormEngine.API.Dependencies;
using Kdega.ScormEngine.API.Extensions;
using Kdega.ScormEngine.Application.Behavior.ExceptionBehavior;
using Kdega.ScormEngine.Application.Behavior.Logging;
using Serilog;
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureSerilog(builder.Configuration, builder.Environment);

    builder.Services.AddControllers()
        .AddCustomApiBehavior();

    builder.Services.AddEndpointsApiExplorer();

    builder.AddDependencyServices();


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (!app.Environment.IsTesting())
        app.MigrateDatabase();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ExceptionMiddleware>();

    app.Run();
}
catch (Exception ex)
{

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
public partial class Program { }
