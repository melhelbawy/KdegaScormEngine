using Kdega.ScormEngine.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.API.Dependencies;
public static class DatabaseMigration
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IKseDbContext>();
        context.Database.Migrate();
    }
}
