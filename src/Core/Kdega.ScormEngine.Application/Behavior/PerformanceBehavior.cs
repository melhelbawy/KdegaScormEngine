﻿using Kdega.ScormEngine.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Kdega.ScormEngine.Application.Behavior;
public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly CurrentUserService _currentUserService;

    public PerformanceBehavior(
        ILogger<TRequest> logger,
        CurrentUserService currentUserService
    )
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500) return response;

        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId;

        _logger.LogWarning("KSE Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
            requestName, elapsedMilliseconds, userId, request);

        return response;
    }
}
