namespace Kdega.ScormEngine.Application.Common.Models;
public class StorageObject
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public bool IsDirectory { get; set; } = false;
    public string? Permissions { get; set; }
    public List<StorageObject> SubDirectory { get; set; } = new();
}
