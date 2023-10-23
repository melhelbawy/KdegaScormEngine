using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

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
