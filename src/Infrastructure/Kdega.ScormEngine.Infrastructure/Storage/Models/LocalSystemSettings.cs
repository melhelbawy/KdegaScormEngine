namespace Kdega.ScormEngine.Infrastructure.Storage.Models;
public class LocalSystemSettings
{
    public string MainDirectory { get; set; } = null!;
    public string LinuxRootDirectory { get; set; } = null!;
    public string WindowsRootDirectory { get; set; } = null!;
    public char Separator => '/';
}
