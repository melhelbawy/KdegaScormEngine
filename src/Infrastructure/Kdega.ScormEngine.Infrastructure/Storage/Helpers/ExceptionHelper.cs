namespace Kdega.ScormEngine.Infrastructure.Storage.Helpers;
public static class ExceptionHelper
{
    public static void ThrowIfNotValidHostNameOrIpAddress(this string host)
    {
        ArgumentNullException.ThrowIfNull(host);
        if (host.StartsWith("http") || host.StartsWith("ftp"))
            throw new ArgumentException("Host should contain only 'HostName' or 'IpAddress' without Schema");
    }

    public static void ThrowIfNotValidPort(this int port)
    {
        ArgumentNullException.ThrowIfNull(port);

        if (port is < 1 or > 65535)
            throw new ArgumentException($"Port {port} is not a number between 1 and 65535");
    }

    public static void ThrowIsNotNullOrEmpty(this string val, string paramName)
    {
        ArgumentException.ThrowIfNullOrEmpty(paramName);
        ArgumentException.ThrowIfNullOrWhiteSpace(paramName);
    }
}
