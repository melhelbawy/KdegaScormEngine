using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Queries;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.comments_from_lms ", ScormApiMethod.Get)]
public class GetCmiCommentsFromLmsQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCommentsFromLmsQueryHandler : BaseHandler, IRequestHandler<GetCmiCommentsFromLmsQuery, LmsRequest>
{
    public GetCmiCommentsFromLmsQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(GetCmiCommentsFromLmsQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiData
            .FirstOrDefaultAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        request.LmsRequest.InitCode0Success(cmiCore.CmiCommentsFromLms ?? "");
        return request.LmsRequest;
    }
}
#endregion
