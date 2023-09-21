namespace Kdega.ScormEngine.Application.Common.Models;
public class StorageObject
{
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsDirectory { get; set; }
    public string? Permissions { get; set; }
    public List<StorageObject> SubDirectory { get; set; } = new();
}
