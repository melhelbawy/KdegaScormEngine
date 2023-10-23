using Kdega.ScormEngine.Domain.Constants.ScormLms;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Models;
public class PackageLaunchParams
{
    public Guid Id { get; set; }
    public string LearnerId { get; set; } = null!;
    public Guid CmiCoreId { get; set; }
    public string IndexPath { get; set; } = string.Empty;
    public string FolderPath { get; set; } = string.Empty;
    public string LaunchUri => $"{FolderPath}/{IndexPath}";
    public string DefaultOrganizationIdentifier { get; set; } = LmsClient.ScoIdentifier;
    public string ScormVersion { get; set; } = LmsClient.ScormVersion;
}
