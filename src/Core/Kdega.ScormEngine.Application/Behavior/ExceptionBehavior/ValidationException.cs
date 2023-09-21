using Kdega.ScormEngine.Application.Behavior.Models;

namespace Kdega.ScormEngine.Application.Behavior.ExceptionBehavior;
public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ErrorRecord> errors)
    {
        Errors = errors;
    }

    public string ErrorMessage { get; private set; } = "Validation format failed. Please return to documentations";
    public IEnumerable<ErrorRecord> Errors { get; private set; }
}