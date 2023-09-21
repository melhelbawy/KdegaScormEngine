namespace Kdega.ScormEngine.Application.Behavior.Models;
public record FailureResponse
{
    public string Message { get; set; }
    public List<ErrorRecord> Errors { get; set; }

    public FailureResponse(string message, List<ErrorRecord>? errors)
    {
        Message = message;
        Errors = errors ?? new List<ErrorRecord>();
    }
}
