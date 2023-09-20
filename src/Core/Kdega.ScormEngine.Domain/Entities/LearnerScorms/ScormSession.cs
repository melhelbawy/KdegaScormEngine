using Kdega.ScormEngine.Domain.Constants.ScormLms;
using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("session")]
public class ScormSession : AuditableEntity
{
    [StringLength(100)]
    [JsonProperty("session_id")]
    public string? SessionId { get; set; } = LmsClient.SessionId;
    [JsonProperty("learner_id")]
    public string? LearnerId { get; set; }
    [JsonProperty("start_datetime")]
    public DateTime? StartDatetime { get; set; }
    [JsonProperty("end_datetime")]
    public DateTime? EndDatetime { get; set; }

    [StringLength(100)]
    [JsonProperty("Sco_identifier")]
    public string? ScoIdentifier { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
