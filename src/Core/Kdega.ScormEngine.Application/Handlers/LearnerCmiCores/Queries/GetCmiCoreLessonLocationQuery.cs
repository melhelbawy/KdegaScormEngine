using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.lesson_location", ScormApiMethod.Get)]
public class GetCmiCoreLessonLocationQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreLessonLocationQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreLessonLocationQuery, LmsRequest>
{
    public GetCmiCoreLessonLocationQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCoreLessonLocationQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.LessonLocation ?? "");
        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.location", ScormApiMethod.Get)]
public class GetCmiLocationQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiLocationQueryHandler : BaseHandler, IRequestHandler<GetCmiLocationQuery, LmsRequest>
{
    public GetCmiLocationQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiLocationQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.Location ?? "");
        return request.LmsRequest;
    }
}
#endregion
