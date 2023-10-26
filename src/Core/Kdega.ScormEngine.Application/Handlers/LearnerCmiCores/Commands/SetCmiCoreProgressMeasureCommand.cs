using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

[ScormMediatorComponent("cmi.progress_measure", ScormApiMethod.Set)]
public class SetCmiProgressMeasureTmeCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiProgressMeasureTmeCommandHandler : BaseHandler, IRequestHandler<SetCmiProgressMeasureTmeCommand, LmsRequest>
{
    public SetCmiProgressMeasureTmeCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiProgressMeasureTmeCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiDecimal(request.LmsRequest.DataValue!))
        {
            cmiCore!.ProgressMeasure = Convert.ToDecimal(request.LmsRequest.DataValue);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }


}
