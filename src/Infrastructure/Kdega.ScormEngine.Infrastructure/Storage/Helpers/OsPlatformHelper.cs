using System.Runtime.InteropServices;

namespace Kdega.ScormEngine.Infrastructure.Storage.Helpers;
public static class OsPlatformHelper
{
    public static OSPlatform? GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows;
        }
        return null;
    }
}
