using Kdega.ScormEngine.Domain.Entities.Base;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;

[JsonObject("Scorm_Package")]
public class ScormPackage : AuditableEntity
{

    [JsonProperty("title_from_manifest")]
    public string? TitleFromManifest { get; set; }
    [JsonProperty("title_from_upload")]
    public string? TitleFromUpload { get; set; }
    [JsonProperty("path_to_Lms_index")]
    public string? IndexPath { get; set; }
    [JsonProperty("package_folder_path")]
    public string? PackageFolderPath { get; set; }
    [JsonProperty("uploading_date")]
    public DateTime UploadingDate { get; set; }

    [JsonProperty("Scorm_version")]
    public string? ScormVersion { get; set; }

    [JsonProperty("manifest_identifier")]
    public string ManifestIdentifier { get; set; } = null!;
    [JsonProperty("manifest_xml_base")]
    public string? ManifestXmlBase { get; set; }
    [JsonProperty("manifest_version")]
    public string? ManifestVersion { get; set; }
    [JsonProperty("manifest_metadata_schema")]
    public string ManifestMetadataSchema { get; set; } = null!;
    [JsonProperty("manifest_metadata_schema_version")]
    public string ManifestMetadataSchemaVersion { get; set; } = null!;
    [JsonProperty("manifest_metadata_location")]
    public string? ManifestMetadataLocation { get; set; }
    [JsonProperty("manifest_organization_default")]
    public string? DefaultOrganizationIdentifier { get; set; }

    public string? ResourceXmlBase { get; set; }
    public virtual ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<LearnerScormPackage> LearnersScorms { get; set; } = new HashSet<LearnerScormPackage>();
}
