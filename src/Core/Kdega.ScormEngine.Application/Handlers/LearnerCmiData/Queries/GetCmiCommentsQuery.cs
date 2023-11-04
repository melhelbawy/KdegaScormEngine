using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.comments", ScormApiMethod.Get)]
public class GetCmiCommentsQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCommentsQueryHandler : BaseHandler, IRequestHandler<GetCmiCommentsQuery, LmsRequest>
{
    public GetCmiCommentsQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCommentsQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiData
            .FirstOrDefaultAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.CmiComments ?? "");
        return request.LmsRequest;
    }
}
#endregion
