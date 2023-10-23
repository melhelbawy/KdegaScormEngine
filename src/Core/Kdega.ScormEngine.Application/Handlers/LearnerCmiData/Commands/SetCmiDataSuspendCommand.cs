using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Commands;

[ScormMediatorComponent("cmi.suspend_data", ScormApiMethod.Set)]
public class SetCmiDataSuspendCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiDataSuspendCommandHandler : BaseHandler, IRequestHandler<SetCmiDataSuspendCommand, LmsRequest>
{
    public SetCmiDataSuspendCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiDataSuspendCommand request, CancellationToken cancellationToken)
    {
        var cmiData = await Context.CmiData
            .FirstOrDefaultAsync(x => x.CmiCoreId == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiString4096(request.LmsRequest.DataValue!))
        {
            if (cmiData is not null)
            {
                cmiData!.SuspendData = request.LmsRequest.DataValue;
            }
            else
            {
                await Context.CmiData.AddAsync(new CmiData()
                {
                    CmiCoreId = Guid.Parse(request.LmsRequest.CoreId),
                    LearnerId = request.LmsRequest.LearnerId,
                    SuspendData = request.LmsRequest.DataValue
                }, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);

        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }


}
