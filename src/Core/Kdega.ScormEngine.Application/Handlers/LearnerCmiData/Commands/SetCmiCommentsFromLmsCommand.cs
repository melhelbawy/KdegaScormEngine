using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Commands;

#region Scorm Versions 1.1 and 1.2

[ScormMediatorComponent("cmi.comments_from_lms", ScormApiMethod.Set)]
public class SetCmiCommentsCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCommentsCommandHandler : BaseHandler, IRequestHandler<SetCmiCommentsCommand, LmsRequest>
{
    public SetCmiCommentsCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiCommentsCommand request, CancellationToken cancellationToken)
    {
        var cmiData = await Context.CmiData
            .Include(x => x.CmiCore)
            .FirstOrDefaultAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiString4096(request.LmsRequest.DataValue!))
        {
            cmiData!.CmiComments = request.LmsRequest.DataValue;
            cmiData.CmiCore.Comments = request.LmsRequest.DataValue;

            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }

}
#endregion