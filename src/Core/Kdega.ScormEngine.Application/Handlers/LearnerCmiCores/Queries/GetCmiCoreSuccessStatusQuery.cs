using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
[ScormMediatorComponent("cmi.success_status", ScormApiMethod.Get)]
public class GetCmiCoreSuccessStatusQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreSuccessStatusQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreSuccessStatusQuery, LmsRequest>
{
    public GetCmiCoreSuccessStatusQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetCmiCoreSuccessStatusQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FindAsync(request.LmsRequest.CoreId);
        request.LmsRequest.InitCode0Success(cmiCore!.SuccessStatus.IsNullOrEmpty()
            ? ScormCmiCore.CmiCoreSuccessStatus.Unknown : cmiCore.SuccessStatus!);
        return request.LmsRequest;
    }


}