using Kdega.ScormEngine.Application.Behavior.ExceptionBehavior;
using Kdega.ScormEngine.Application.Behavior.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Kdega.ScormEngine.Application.Behavior.Interceptors;
public class ErrorActionResult : IActionResult
{
    public IEnumerable<ModelError> Errors { get; set; }

    public ErrorActionResult(IEnumerable<ModelError> errors)
    {
        Errors = errors;
    }
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var errors = Errors.Select(e =>
        {
            try
            {
                return JsonSerializer.Deserialize<ErrorRecord>(e.ErrorMessage);
            }
            catch
            {
                return new ErrorRecord("", "NotValidData", e.ErrorMessage);
            }
        }).ToList();

        if (errors.Any())
        {
            await Task.Run(() => throw new ValidationException(errors!));
        }
    }
}
