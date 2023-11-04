using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Queries;
[ScormMediatorComponent("cmi.launch_data", ScormApiMethod.Get)]
public class GetLaunchDataQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetLaunchDataQueryHandler : BaseHandler, IRequestHandler<GetLaunchDataQuery, LmsRequest>
{
    public GetLaunchDataQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetLaunchDataQuery request, CancellationToken cancellationToken)
    {
        var cmiData = await Context.CmiData.FirstAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);
        request.LmsRequest.InitCode0Success(cmiData.LaunchData ?? "");
        return request.LmsRequest;
    }
}