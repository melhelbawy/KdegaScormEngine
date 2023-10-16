using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kdega.ScormEngine.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class BaseController : Controller
{
    protected bool IsAuthenticated => (User.Identity?.IsAuthenticated)!.Value;
    protected string UserId => GetUserId()!;

    protected readonly IMediator Mediator;

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected async Task<IActionResult> Send<TRequest>(TRequest command)
        where TRequest : class
    {
        return Ok(await Mediator.Send(command));
    }

    protected Task<TResponse> Send<TRequest, TResponse>(TRequest command)
        where TRequest : IRequest<TResponse> where TResponse : class
    {
        return Task.Run(() => Mediator.Send<TResponse>(command));
    }

    private string? GetUserId()
    {
        return IsAuthenticated ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : null;
    }
}
