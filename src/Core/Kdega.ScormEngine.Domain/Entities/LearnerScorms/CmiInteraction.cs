using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_interactions")]
public class CmiInteraction : AuditableEntity
{
    [JsonProperty("_count")]
    public int? Count { get; set; }
    [JsonProperty("n")]
    public int? N { get; set; }
    [StringLength(255)]
    [JsonProperty("n_id")]
    public string? NId { get; set; }
    [StringLength(50)]
    [JsonProperty("interaction_time")]
    public string? InteractionTime { get; set; }
    [StringLength(11)]
    [JsonProperty("type")]
    public string? Type { get; set; }
    [StringLength(50)]
    [JsonProperty("weighting")]
    public string? Weighting { get; set; }
    [StringLength(8000)]
    [JsonProperty("student_response")]
    public string? StudentResponse { get; set; }
    [StringLength(13)]
    [JsonProperty("result")]
    public string? Result { get; set; }
    [StringLength(13)]
    [JsonProperty("latency")]
    public string? Latency { get; set; }
    [JsonProperty("msrepl_tran_version")]
    public Guid? MsreplTranVersion { get; set; }
    [StringLength(255)]
    [JsonProperty("description")]
    public string? Description { get; set; }
    [StringLength(20)]
    [JsonProperty("timestamp")]
    public string? Timestamp { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
