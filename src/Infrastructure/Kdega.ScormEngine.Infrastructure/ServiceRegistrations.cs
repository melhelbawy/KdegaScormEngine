using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Infrastructure.Storage.LocalSystemStorage;
using Kdega.ScormEngine.Infrastructure.Storage.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kdega.ScormEngine.Infrastructure;
public static class ServiceRegistrations
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LocalSystemSettings>(configuration.GetSection("LocalSystemSettings"));
        services.AddSingleton<IDirectoryManger, LocalSystemDirectoryManager>();
        services.AddSingleton<IObjectManager, LocalSystemStorageProvider>();
    }
}
