using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

#region Scorm Versions 1.1 and 1.2
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
            var currentTotalTime = cmiCore!.TotalTime;
            cmiCore!.SessionTime = request.LmsRequest.DataValue;
            cmiCore.TotalTime = ScormDataValidatorHelper.AddCmiTime(request.LmsRequest.DataValue, currentTotalTime);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}

#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.session_time", ScormApiMethod.Set)]
public class SetCmiSessionTmeCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiSessionTmeCommandHandler : BaseHandler, IRequestHandler<SetCmiSessionTmeCommand, LmsRequest>
{
    public SetCmiSessionTmeCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiSessionTmeCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);


        if (ScormDataValidatorHelper.IsCmiTimespan(request.LmsRequest.DataValue!))
        {
            var currentTotalTime = cmiCore!.TotalTime;
            cmiCore!.SessionTime = request.LmsRequest.DataValue;
            cmiCore.TotalTime = ScormDataValidatorHelper.AddCmiTime(request.LmsRequest.DataValue, currentTotalTime);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
#endregion
