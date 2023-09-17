using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_comment_from_lms")]
public class CmiCommentFromLms : AuditableEntity
{
    [JsonProperty("_count")]
    public int? Count { get; set; }
    [JsonProperty("n")]
    public int? N { get; set; }
    [StringLength(4096)]
    [JsonProperty("n.comment")]
    public string? NComment { get; set; }
    [StringLength(250)]
    [JsonProperty("n.location")]
    public string? NLocation { get; set; }
    [StringLength(50)]
    [JsonProperty("n.timestamp")]
    public string? NTimestamp { get; set; }

    [StringLength(255)]
    [JsonProperty("Sco_identifier")]
    public string? ScoIdentifier { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
