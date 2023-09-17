using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_interactions_objectives")]
public class CmiInteractionsObjective : AuditableEntity
{
    [JsonProperty("n")]
    public int? N { get; set; }
    [JsonProperty("interaction_n")]
    public int? InteractionN { get; set; }
    [StringLength(255)]
    [JsonProperty("objective_id")]
    public string? ObjectiveId { get; set; }
    [JsonProperty("msrepl_tran_version")]
    public Guid? MsreplTranVersion { get; set; }


    [JsonProperty("interactions_id")]
    public Guid InteractionId { get; set; }
    [JsonProperty("cmi_interaction")]
    public CmiInteraction CmiInteraction { get; set; } = null!;

}
