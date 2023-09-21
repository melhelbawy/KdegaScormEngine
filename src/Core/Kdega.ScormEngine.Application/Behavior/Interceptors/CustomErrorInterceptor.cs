using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Kdega.ScormEngine.Application.Behavior.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Kdega.ScormEngine.Application.Behavior.Interceptors;
public class CustomErrorInterceptor : IValidatorInterceptor
{
    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext) =>
        commonContext;

    /// <summary>
    /// Catch all validate errors.
    /// </summary>
    /// <param name="actionContext"></param>
    /// <param name="validationContext"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext,
        ValidationResult result)
    {
        var failures = result.Errors
            .Select(error => new ValidationFailure(error.PropertyName, SerializeError(error)));

        return new ValidationResult(failures);
    }
    /// <summary>
    /// Serialize Error message to add extra parameters
    /// </summary>
    /// <param name="failure"></param>
    /// <returns></returns>
    private static string SerializeError(ValidationFailure failure)
    {
        var error = new ErrorRecord(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage);
        return JsonSerializer.Serialize(error);
    }
}
