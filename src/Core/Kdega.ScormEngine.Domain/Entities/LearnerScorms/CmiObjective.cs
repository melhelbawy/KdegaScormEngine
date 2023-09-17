using Kdega.ScormEngine.Domain.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;

[JsonObject("cmi_objectives")]
public class CmiObjective : AuditableEntity
{
    [JsonProperty("_count")]
    public int? Count { get; set; }
    [JsonProperty("n")]
    public int? N { get; set; }
    [StringLength(255)]
    [JsonProperty("n_id")]
    public string? NId { get; set; }
    [JsonProperty("n.score_raw")]
    public decimal? NScoreRaw { get; set; }
    [JsonProperty("n.score_min")]
    public decimal? NScoreMin { get; set; }
    [JsonProperty("n.score_max")]
    public decimal? NScoreMax { get; set; }
    [JsonProperty("n.status")]
    public string? NStatus { get; set; }
    [JsonProperty("n.score.scaled")]
    public decimal? NScoreScaled { get; set; }
    [JsonProperty("n.success_status")]
    public string? NSuccessStatus { get; set; }
    [JsonProperty("n.completion_status")]
    public string? NCompletionStatus { get; set; }
    [JsonProperty("n.progress_measure")]
    public string? NProgressMeasure { get; set; }
    [StringLength(250)]
    [JsonProperty("description")]
    public string? Description { get; set; }
    [JsonProperty("msrepl_tran_version")]
    public Guid? MsreplTranVersion { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
