using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.student_id", ScormApiMethod.Get)]
public class GetCmiCoreLearnerIdQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreLearnerIdQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreLearnerIdQuery, LmsRequest>
{
    public GetCmiCoreLearnerIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCoreLearnerIdQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.LearnerId ?? "");
        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.learner_id", ScormApiMethod.Get)]
public class GetCmiLearnerIdQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmLearnerIdQueryHandler : BaseHandler, IRequestHandler<GetCmiLearnerIdQuery, LmsRequest>
{
    public GetCmLearnerIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiLearnerIdQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.LearnerId ?? "");
        return request.LmsRequest;
    }
}
#endregion
