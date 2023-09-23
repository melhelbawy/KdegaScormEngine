namespace Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
public class LmsClientDto
{
    public string? SessionId { get; set; }
    public string? LearnerId { get; set; }
    public string? CoreId { get; set; }
    public string? ScormLearnerPackageId { get; set; }
    public string? ScoIdentifier { get; set; }
    public string? ScormPackageId { get; set; }
    public string? ScoAddress { get; set; }
    public string? ScoFrameClientId { get; set; }
    public string? DivDebugId { get; set; }
    public string? BDebug { get; set; }
    public string? DateCreated { get; set; } = DateTime.Now.ToString("s");
}
