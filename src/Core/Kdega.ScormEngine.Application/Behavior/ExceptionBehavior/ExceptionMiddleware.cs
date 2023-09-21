using Kdega.ScormEngine.Application.Behavior.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Kdega.ScormEngine.Application.Behavior.ExceptionBehavior;
public class ExceptionMiddleware : IMiddleware
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger _logger;

    public ExceptionMiddleware(IHostEnvironment hostEnvironment, ILogger logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex);
        }
    }
    private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = exception switch
        {
            ValidationException ex => HandleValidationException(context, ex),
            ArgumentNullException ex => HandleArgumentException(context, ex),
            ArgumentException ex => HandleArgumentException(context, ex),
            ApplicationException ex => HandleApplicationException(context, ex),
            UnauthorizedAccessException ex => HandleUnauthorized(context, ex),
            _ => HandleException(context, exception),
        };
        var result = JsonConvert.SerializeObject(response);
        return context.Response.WriteAsync(result);
    }

    private FailureResponse HandleValidationException(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        _logger.Error(exception, exception.Message);
        return new FailureResponse(exception.ErrorMessage, exception.Errors.ToList());
    }

    private FailureResponse HandleArgumentException(HttpContext context, ArgumentException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new FailureResponse(exception.Message, null);
    }

    private FailureResponse HandleException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        if (exception.Message.Contains("inner exception"))
        {
            if (_hostEnvironment.IsDevelopment())
                return new FailureResponse(exception.InnerException?.ToString() ?? exception.Message, null);
        }
        return new FailureResponse(exception.Message, null);
    }

    private FailureResponse HandleApplicationException(HttpContext context, ApplicationException exception)
    {
        if (!exception.Message.Contains("Unauthorized"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new FailureResponse(exception.Message, null);
        }
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        _logger.Error(exception, exception.Message);
        return new FailureResponse(exception.Message, null);
    }
    private FailureResponse HandleUnauthorized(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        _logger.Error(exception, exception.Message);
        return new FailureResponse(exception.Message, null);
    }
}
