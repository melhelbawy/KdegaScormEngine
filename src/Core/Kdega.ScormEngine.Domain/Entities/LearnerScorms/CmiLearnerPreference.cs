using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_student_preferences")]
public class CmiLearnerPreference : AuditableEntity
{
    [JsonProperty("audio_level")]
    public decimal? AudioLevel { get; set; }
    [JsonProperty("audio_captioning")]
    public int? AudioCaptioning { get; set; }
    [StringLength(255)]
    [JsonProperty("language")]
    public string? Language { get; set; }
    [JsonProperty("delivery_speed")]
    public decimal? DeliverySpeed { get; set; }
    [JsonProperty("text")]
    public int? Text { get; set; }
    [JsonProperty("msrepl_tran_version")]
    public Guid? MsreplTranVersion { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
