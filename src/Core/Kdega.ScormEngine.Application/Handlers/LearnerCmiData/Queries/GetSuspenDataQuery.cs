using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Queries;
[ScormMediatorComponent("cmi.suspend_data", ScormApiMethod.Get)]
public class GetSuspendDataQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetSuspendDataQueryHandler : BaseHandler, IRequestHandler<GetSuspendDataQuery, LmsRequest>
{
    public GetSuspendDataQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetSuspendDataQuery request, CancellationToken cancellationToken)
    {
        var cmiData = await Context.CmiData.FirstAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);
        request.LmsRequest.InitCode0Success(cmiData.SuspendData ?? "");
        return request.LmsRequest;
    }


}