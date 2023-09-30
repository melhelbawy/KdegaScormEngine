using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
public class UpdateCmiCoreExitCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; }

    public UpdateCmiCoreExitCommand(LmsRequest lmsRequest)
    {
        LmsRequest = lmsRequest;
    }
}

public class UpdateCmiCoreExitCommandHandler : BaseHandler<CmiCore>, IRequestHandler<UpdateCmiCoreExitCommand, LmsRequest>
{
    public UpdateCmiCoreExitCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(UpdateCmiCoreExitCommand request, CancellationToken cancellationToken)
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
