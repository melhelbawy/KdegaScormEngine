using Newtonsoft.Json;

namespace Kdega.ScormEngine.Application.Common.Models;
public class LmsRequest
{
    /// <summary>
    /// SessionID for identification purposes
    /// </summary>
    [JsonProperty("session_id")]
    public string SessionId { get; set; } = null!;
    /// <summary>
    /// user_id
    /// </summary>
    [JsonProperty("learner_id")]
    public string? LearnerId { get; set; }
    [JsonProperty("core_id")]
    public string? CoreId { get; set; }
    /// <summary>
    /// Identifier for the SCO (from the manifest, not guaranteed to be unique)
    /// </summary>
    [JsonProperty("sco_identifier")]
    public string? ScoIdentifier { get; set; }
    /// <summary>
    /// Identifier for the SCORM course
    /// </summary>
    [JsonProperty("Scorm_content_id")]
    public string? ScormContentId { get; set; }
    /// <summary>
    /// DataItem for LMSSet/Get Calls
    /// </summary>
    [JsonProperty("dataItem")]
    public string? DataItem { get; set; }
    /// <summary>
    /// Data value for LMSSet/Get calls
    /// </summary>
    [JsonProperty("dataValue")]
    public string? DataValue { get; set; }
    /// <summary>
    /// Error Code (Or "0" for no error)
    /// </summary>
    [JsonProperty("errorCode")]
    public string? ErrorCode { get; set; }
    /// <summary>
    /// Error String corresponding to ErrorCode
    /// </summary>
    [JsonProperty("errorString")]
    public string? ErrorString { get; set; }
    /// <summary>
    /// Error Diagnostic - additional info about the error
    /// </summary>
    [JsonProperty("errorDiagnostic")]
    public string? ErrorDiagnostic { get; set; }
    /// <summary>
    /// Value to be returned to caller (sometimes this is just "true" or "false")
    /// </summary>
    [JsonProperty("returnValue")]
    public string? ReturnValue { get; set; }
}
