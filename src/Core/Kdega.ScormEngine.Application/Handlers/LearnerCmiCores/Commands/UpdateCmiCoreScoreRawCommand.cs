using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

[ScormMediatorComponent("cmi.core.score.raw")]
public class UpdateCmiCoreScoreRawCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class UpdateCmiCoreScoreRawCommandHandler : BaseHandler<CmiCore>, IRequestHandler<UpdateCmiCoreScoreRawCommand, LmsRequest>
{
    public UpdateCmiCoreScoreRawCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(UpdateCmiCoreScoreRawCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        Check.NotNull(cmiCore, nameof(CmiCore));

        if (ScormDataValidatorHelper.IsCmiVocabulary("exit", request.LmsRequest.DataValue!))
        {
            cmiCore.Exit = request.LmsRequest.DataValue;
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
