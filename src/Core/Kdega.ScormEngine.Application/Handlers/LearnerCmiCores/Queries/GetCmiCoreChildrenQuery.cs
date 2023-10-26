using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
[ScormMediatorComponent("cmi.core.score_children", ScormApiMethod.Get)]
public class GetCmiCoreChildrenQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreChildrenQueryHandler : IRequestHandler<GetCmiCoreChildrenQuery, LmsRequest>
{
    public Task<LmsRequest> Handle(GetCmiCoreChildrenQuery request, CancellationToken cancellationToken)
    {
        request.LmsRequest.InitCode0Success(ScormCmiCore.CmiChildren);
        return Task.FromResult(request.LmsRequest);
    }
}