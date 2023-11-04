using Kdega.ScormEngine.Domain.Constants.ScormLms;

namespace Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
public class LmsClientParamsDto
{
    public string ScormLearnerPackageId { get; set; } = null!;
    public string IndexPath { get; set; } = string.Empty;
    public string FolderPath { get; set; } = string.Empty;
    public string LaunchUri => $"{FolderPath}/{IndexPath}";
    public string ScoIdentifier { get; set; } = LmsClient.ScoIdentifier;
    public string ScormVersion { get; set; } = LmsClient.ScormVersion;
    public string? SessionId { get; set; }
    public string ScoFrameClientId => LmsClient.ScoFrameClientId;
    public string DivDebug => LmsClient.DivDebug;
    public string BDebug => LmsClient.BDebug;
    public string? DateCreated { get; set; } = DateTime.Now.ToString("s");
}
