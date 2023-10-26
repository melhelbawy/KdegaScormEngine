using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;


#region  Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.lesson_mode", ScormApiMethod.Get)]
public class GetCmiCoreLessonModeQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreLessonModeQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreLessonModeQuery, LmsRequest>
{
    public GetCmiCoreLessonModeQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetCmiCoreLessonModeQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FindAsync(Guid.Parse(request.LmsRequest.CoreId));
        request.LmsRequest.InitCode0Success(cmiCore!.LessonMode ?? ScormCmiCore.CmiCoreLessonMode.Normal);
        return request.LmsRequest;
    }
}

#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.mode", ScormApiMethod.Get)]
public class GetCmiCoreModeQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreModeQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreModeQuery, LmsRequest>
{
    public GetCmiCoreModeQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetCmiCoreModeQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FindAsync(Guid.Parse(request.LmsRequest.CoreId));
        request.LmsRequest.InitCode0Success(cmiCore!.LessonMode ?? ScormCmiCore.CmiCoreLessonMode.Normal);
        return request.LmsRequest;
    }
}

#endregion