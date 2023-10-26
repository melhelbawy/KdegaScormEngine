using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.student_id", ScormApiMethod.Get)]
public class GetCurrentStudentIdQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCurrentStudentIdQueryHandler : BaseHandler, IRequestHandler<GetCurrentStudentIdQuery, LmsRequest>
{
    public GetCurrentStudentIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCurrentStudentIdQuery request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.LearnerScormPackageId!), cancellationToken);

        request.LmsRequest.InitCode0Success(learnerScormPackage.LearnerId ?? "");
        return request.LmsRequest;
    }
}

#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition

[ScormMediatorComponent("cmi.cmi.learner_id ", ScormApiMethod.Get)]
public class GetCurrentLearnerIdQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCurrentLearnerIdQueryHandler : BaseHandler, IRequestHandler<GetCurrentLearnerIdQuery, LmsRequest>
{
    public GetCurrentLearnerIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCurrentLearnerIdQuery request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.LearnerScormPackageId!), cancellationToken);

        request.LmsRequest.InitCode0Success(learnerScormPackage.LearnerId ?? "");
        return request.LmsRequest;
    }
}
#endregion