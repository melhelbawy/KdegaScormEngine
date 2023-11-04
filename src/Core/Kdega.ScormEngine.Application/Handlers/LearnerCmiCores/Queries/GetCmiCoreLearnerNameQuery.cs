using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.student_name", ScormApiMethod.Get)]
public class GetCmiCoreLearnerNameQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreLearnerNameQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreLearnerNameQuery, LmsRequest>
{
    public GetCmiCoreLearnerNameQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCoreLearnerNameQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.StudentName ?? "");
        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.learner_name", ScormApiMethod.Get)]
public class GetCmiLearnerNameQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiLearnerNameQueryHandler : BaseHandler, IRequestHandler<GetCmiLearnerNameQuery, LmsRequest>
{
    public GetCmiLearnerNameQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiLearnerNameQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.StudentName ?? "");
        return request.LmsRequest;
    }
}
#endregion
