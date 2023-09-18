using Kdega.ScormEngine.Domain.Entities.Base;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;
public class LearnerScormPackage : AuditableEntity
{
    [JsonProperty("learner_id")]
    public string? LearnerId { get; set; }
    [JsonProperty("joining_date")]
    public DateTime? JoiningDate { get; set; }
    [JsonProperty("completion_date")]
    public DateTime? CompletionDate { get; set; }
    [JsonProperty("date_passed")]
    public DateTime? DatePassed { get; set; }
    [JsonProperty("last_opened_date")]
    public DateTime? LastOpenedDate { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    [JsonProperty("score")]
    public decimal? Score { get; set; }

    [JsonProperty("scorm_package_id")]
    public Guid ScormPackageId { get; set; }

    [JsonProperty("cmi_core_id")]
    public Guid CmiCoreId { get; set; }

    [JsonProperty("Scorm_Package")]
    public ScormPackage ScormPackage { get; set; } = null!;
    [JsonProperty("cmi_core")]
    public CmiCore CmiCore { get; set; } = null!;
}
