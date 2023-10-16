using Kdega.ScormEngine.Domain.Entities.Base;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kdega.ScormEngine.Domain.Entities.LearnerScorms;


[JsonObject("cmi_core")]
public class CmiCore : AuditableEntity
{
    [JsonProperty("_count")]
    public int? Count { get; set; }
    [JsonProperty("student_id")]
    public string? LearnerId { get; set; }
    [StringLength(255)]
    [JsonProperty("student_name")]
    public string? StudentName { get; set; }
    [StringLength(1000)]
    [JsonProperty("lesson_location")]
    public string? LessonLocation { get; set; }
    [StringLength(9)]
    [JsonProperty("credit")]
    public string? Credit { get; set; }
    [StringLength(13)]
    [JsonProperty("lesson_status")]
    public string? LessonStatus { get; set; }
    [StringLength(9)]
    [JsonProperty("entry")]
    public string? Entry { get; set; }
    [JsonProperty("score_children")]
    public string? ScoreChildren { get; set; }
    [JsonProperty("score_raw")]
    public decimal? ScoreRaw { get; set; }
    [JsonProperty("score_min")]
    public decimal? ScoreMin { get; set; }
    [JsonProperty("score_max")]
    public decimal? ScoreMax { get; set; }
    [StringLength(20)]
    [JsonProperty("total_time")]
    public string? TotalTime { get; set; }
    [StringLength(6)]
    [JsonProperty("lesson_mode")]
    public string? LessonMode { get; set; }
    [StringLength(8)]
    [JsonProperty("exit")]
    public string? Exit { get; set; }
    [StringLength(20)]
    [JsonProperty("session_time")]
    public string? SessionTime { get; set; }
    [StringLength(450)]
    [JsonProperty("cmi._version")]
    public string? Version { get; set; }
    [StringLength(20)]
    [JsonProperty("completion_status")]
    public string? CompletionStatus { get; set; }
    [JsonProperty("completion_threshold")]
    public decimal? CompletionThreshold { get; set; }
    [JsonProperty("location ")]
    public string? Location { get; set; }
    [JsonProperty("progress_measure ")]
    public decimal? ProgressMeasure { get; set; }
    [JsonProperty("scaled_passing_score ")]
    public decimal? ScaledPassingScore { get; set; }
    [JsonProperty("score_scaled")]
    public decimal? ScoreScaled { get; set; }
    [StringLength(20)]
    [JsonProperty("success_status")]
    public string? SuccessStatus { get; set; }
    [StringLength(4096)]
    [JsonProperty("comments ")]
    public string? Comments { get; set; }
    [JsonProperty("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonProperty("learner_scorm_package_id")]
    public Guid LearnerScormPackageId { get; set; }

    [JsonProperty("learner_scorm_package")]
    public LearnerScormPackage LearnerScormPackage { get; set; } = null!;

    public virtual ICollection<CmiData> CmiData { get; set; } = new HashSet<CmiData>();
    public virtual ICollection<CmiObjective> CmiObjectives { get; set; } = new HashSet<CmiObjective>();
    public virtual ICollection<CmiLearnerPreference> CmiLearnerPreferences { get; set; } = new HashSet<CmiLearnerPreference>();
    public virtual ICollection<CmiInteraction> CmiInteractions { get; set; } = new HashSet<CmiInteraction>();
    public virtual ICollection<CmiCommentFromLearner> CommentFromLearners { get; set; } = new HashSet<CmiCommentFromLearner>();
    public virtual ICollection<CmiCommentFromLms> CommentFromLms { get; set; } = new HashSet<CmiCommentFromLms>();
    public virtual ICollection<ScormSession> ScormSessions { get; set; } = new HashSet<ScormSession>();
}
