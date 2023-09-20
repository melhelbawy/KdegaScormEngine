namespace Kdega.ScormEngine.API.Extensions;
public static class HotEnvironmentExtensions
{
    public static bool IsTesting(this IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsEnvironment("Testing");
}
