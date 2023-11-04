using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.total_time ", ScormApiMethod.Get)]
public class GetCmiCoreTotalTimeQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreTotalTimeQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreTotalTimeQuery, LmsRequest>
{
    public GetCmiCoreTotalTimeQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCoreTotalTimeQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.TotalTime ?? "");
        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.total_time ", ScormApiMethod.Get)]
public class GetCmiTotalTimeQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiTotalTimeQueryHandler : BaseHandler, IRequestHandler<GetCmiTotalTimeQuery, LmsRequest>
{
    public GetCmiTotalTimeQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiTotalTimeQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.TotalTime ?? "");
        return request.LmsRequest;
    }
}
#endregion
