﻿using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LmsSessions.Queries;
public class GetCurrentLearnerSessionIdQuery : IRequest<string>
{
    public string CmiCoreId { get; set; } = null!;
    public string ScormContentId { get; set; } = null!;
    public string LearnerId { get; set; } = null!;
    public string ScoIdentifier { get; set; } = null!;
}

public class GetCurrentLearnerSessionIdQueryHandler : BaseHandler<ScormSession>, IRequestHandler<GetCurrentLearnerSessionIdQuery, string>
{
    public GetCurrentLearnerSessionIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(GetCurrentLearnerSessionIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserSession = await Context.ScormSessions
            .FirstOrDefaultAsync(x => x.LearnerId == request.LearnerId
                                                                      && x.CmiCoreId == Guid.Parse(request.CmiCoreId), cancellationToken);

        Check.NotNull(currentUserSession, "currentUserSession");

        var initSession = await Mediator.Send(new InitLearnerSessionCommand()
        {
            ScormContentId = request.ScormContentId,
            CmiCoreId = request.CmiCoreId,
            ScoIdentifier = request.ScoIdentifier,
            LearnerId = request.LearnerId
        }, cancellationToken);

        return initSession;
    }
}