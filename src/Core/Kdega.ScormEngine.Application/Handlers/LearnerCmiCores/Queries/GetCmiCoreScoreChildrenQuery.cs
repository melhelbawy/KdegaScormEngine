using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
[ScormMediatorComponent("cmi.core._children", ScormApiMethod.Get)]
public class GetCmiCoreScoreChildrenQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreScoreChildrenQueryHandler : IRequestHandler<GetCmiCoreScoreChildrenQuery, LmsRequest>
{
    public Task<LmsRequest> Handle(GetCmiCoreScoreChildrenQuery request, CancellationToken cancellationToken)
    {
        request.LmsRequest.InitCode0Success(ScormCmiCore.CmiScoreChildren);
        return Task.FromResult(request.LmsRequest);
    }
}