using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
#region Scorm Versions 1.1 and 1.2

[ScormMediatorComponent("cmi.core.score.raw", ScormApiMethod.Set)]
public class SetCmiCoreScoreRawCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCoreScoreRawCommandHandler : BaseHandler, IRequestHandler<SetCmiCoreScoreRawCommand, LmsRequest>
{
    public SetCmiCoreScoreRawCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiCoreScoreRawCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiDecimalPositive(request.LmsRequest.DataValue!))
        {
            cmiCore!.ScoreRaw = Convert.ToDecimal(request.LmsRequest.DataValue);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition

[ScormMediatorComponent("cmi.score.raw", ScormApiMethod.Set)]
public class SetCmiScoreRawCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiScoreRawCommandHandler : BaseHandler, IRequestHandler<SetCmiScoreRawCommand, LmsRequest>
{
    public SetCmiScoreRawCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiScoreRawCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiDecimalPositive(request.LmsRequest.DataValue!))
        {
            cmiCore!.ScoreRaw = Convert.ToDecimal(request.LmsRequest.DataValue);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
#endregion
