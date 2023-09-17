using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_interactions_correct_responses")]

public class CmiInteractionsCorrectResponse : AuditableEntity
{
    [JsonProperty("n")]
    public int? N { get; set; }
    [StringLength(8000)]
    [JsonProperty("pattern")]
    public string? Pattern { get; set; }

    [JsonProperty("interaction_id")]
    public Guid InteractionId { get; set; }

    [JsonProperty("cmi_interaction")]
    public CmiInteraction CmiInteraction { get; set; } = null!;
}
