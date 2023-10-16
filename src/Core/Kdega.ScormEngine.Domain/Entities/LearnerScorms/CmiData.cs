using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_data")]
public class CmiData : AuditableEntity
{
    [JsonProperty("learner_id")]
    public string? LearnerId { get; set; }
    [StringLength(450)]
    [JsonProperty("learner_name")]
    public string? LearnerName { get; set; }

    [StringLength(4096)]
    [JsonProperty("suspend_data")]
    public string? SuspendData { get; set; }
    [JsonProperty("launch_data")]
    public string? LaunchData { get; set; }
    [JsonProperty("mastery_score")]
    public decimal? MasteryScore { get; set; }
    [StringLength(14)]
    [JsonProperty("max_time_allowed")]
    public string? MaxTimeAllowed { get; set; }
    [StringLength(19)]
    [JsonProperty("time_limit_action")]
    public string? TimeLimitAction { get; set; }
    [StringLength(50)]
    [JsonProperty("total_time")]
    public string? TotalTime { get; set; }
    [StringLength(50)]
    [JsonProperty("adl.nav.request")]
    public string? AdlNavRequest { get; set; }
    [StringLength(50)]
    [JsonProperty("adl.nav.request_valid.continue")]
    public string? AdlNavRequestValidContinue { get; set; }
    [StringLength(50)]
    [JsonProperty("adl.nav.request_valid.previous")]
    public string? AdlNavRequestValidPrevious { get; set; }
    [StringLength(50)]
    [JsonProperty("adl.nav.request_valid.choice")]
    public string? AdlNavRequestValidChoice { get; set; }
    [StringLength(4096)]
    [JsonProperty("cmi_comments")]
    public string? CmiComments { get; set; }
    [StringLength(8000)]
    [JsonProperty("cmi_comments_from_lms")]
    public string? CmiCommentsFromLms { get; set; }
    [StringLength(100)]
    [JsonProperty("sco_identifier")]
    public string? ScoIdentifier { get; set; }


    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;

}
