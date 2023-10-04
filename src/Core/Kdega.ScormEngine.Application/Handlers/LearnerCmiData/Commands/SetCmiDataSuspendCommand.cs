using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Commands;

[ScormMediatorComponent("cmi.suspend_data")]
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

        if (ScormDataValidatorHelper.IsCmiString255(request.LmsRequest.DataValue!))
        {
            cmiData!.SuspendData = request.LmsRequest.DataValue;
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }


}
