using Kdega.ScormEngine.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Kdega.ScormEngine.Application.Behavior.Logging;
public static class SeriLogConfiguration
{
    public static void ConfigureSerilog(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddLogging(b => b.AddSerilog(ConfigureSerilog(configuration, env)));
    }
    public static void ConfigureSerilog(this IHostBuilder builder, IConfiguration configuration, IHostEnvironment env)
    {
        builder.UseSerilog(ConfigureSerilog(configuration, env));
        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        Log.Information("Start logging");
    }

    private static Logger ConfigureSerilog(IConfiguration configuration, IHostEnvironment env)
    {
        var columnsOptions = new ColumnOptions();
        columnsOptions.Store.Remove(StandardColumn.Id);
        columnsOptions.PrimaryKey.DataType = System.Data.SqlDbType.UniqueIdentifier;

        columnsOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                    {ColumnName = "UserName", PropertyName = "UserName", DataType = SqlDbType.NVarChar, DataLength = 64, AllowNull = true},

                new SqlColumn
                    {ColumnName = "UserId", DataType = SqlDbType.BigInt, NonClusteredIndex = true, AllowNull = true},

                new SqlColumn
                    {ColumnName = "RequestUri", PropertyName= "RequestPath", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = true},
            };


        var connectionString = env.IsDevelopment() ? new SqlServerConfiguration().GetConnectionString() : (configuration.GetSection("SqlServerConfiguration").Get<SqlServerConfiguration>() ?? new SqlServerConfiguration()).GetConnectionString();

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            //.Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(Environment.CurrentDirectory,
                $"log-{Assembly.GetEntryAssembly()!.GetName().Name}-.log"),
                rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .WriteTo
                .Logger(lc => lc.Filter
                .ByIncludingOnly(evt => evt.Level >= Serilog.Events.LogEventLevel.Warning)
                .WriteTo
                .MSSqlServer(connectionString,
                    sinkOptions: new MSSqlServerSinkOptions()
                    {
                        TableName = "EventLogs",
                        AutoCreateSqlTable = false,
                        AutoCreateSqlDatabase = false,
                    },
                    columnOptions: columnsOptions)
                )
            .MinimumLevel.Information()
            .CreateLogger();

        return logger;
    }
}
