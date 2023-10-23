using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

[ScormMediatorComponent("cmi.core.session_time", ScormApiMethod.Set)]
public class SetCmiCoreSessionTmeCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCoreSessionTmeCommandHandler : BaseHandler, IRequestHandler<SetCmiCoreSessionTmeCommand, LmsRequest>
{
    public SetCmiCoreSessionTmeCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiCoreSessionTmeCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiTimespan(request.LmsRequest.DataValue!))
        {
            cmiCore!.SessionTime = request.LmsRequest.DataValue;
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }


}
