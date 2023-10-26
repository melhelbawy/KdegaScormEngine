using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.credit", ScormApiMethod.Get)]
public class GetCmiCoreCreditQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreCreditQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreCreditQuery, LmsRequest>
{
    public GetCmiCoreCreditQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCoreCreditQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.Credit ?? ScormCmiCore.CmiCoreCredits.NoCredit);
        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.credit", ScormApiMethod.Get)]
public class GetCmiCreditQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCreditQueryHandler : BaseHandler, IRequestHandler<GetCmiCreditQuery, LmsRequest>
{
    public GetCmiCreditQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCreditQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.Credit ?? ScormCmiCore.CmiCoreCredits.NoCredit);
        return request.LmsRequest;
    }
}
#endregion
