using Kdega.ScormEngine.Application.Constants;
using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
using Kdega.ScormEngine.Application.Handlers.LmsSessions.Queries;
using Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
using Kdega.ScormEngine.Application.Handlers.ScormContents.Queries;
using Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormClients.Queries;
public class InitLmsClientQuery : IRequest<LmsClientDto>
{
    public string ScormLearnerPackageId { get; set; } = null!;
    public string LearnerId { get; set; } = null!;
}

public class GetScoClientQueryHandler : BaseHandler<CmiCore>, IRequestHandler<InitLmsClientQuery, LmsClientDto>
{
    public GetScoClientQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsClientDto> Handle(InitLmsClientQuery request, CancellationToken cancellationToken)
    {
        var coreId =
            await Mediator.Send(new GetCurrentLearnerCmiCoreIdQuery(Guid.Parse(request.ScormLearnerPackageId)),
                cancellationToken);

        var response = new LmsClientDto
        {
            CoreId = coreId,
            SessionId = await Mediator.Send(new GetCurrentLearnerSessionIdQuery
            { LearnerId = request.LearnerId, CmiCoreId = coreId }, cancellationToken),
            ScoIdentifier = "default",
            LearnerId = request.LearnerId,
            ScormPackageId = request.ScormLearnerPackageId,
            BDebug = LmsClient.BDebug,
            DivDebugId = LmsClient.DivDebug,
            ScoFrameClientId = LmsClient.ScoFrameClientId,
            ScormLearnerPackageId = await Mediator.Send(new GetCurrentLearnerPackageIdQuery()
            {
                LearnerId = request.LearnerId,
                ScormContentId = request.ScormLearnerPackageId
            }, cancellationToken),
            ScoAddress = await Mediator.Send(new GetContentHrefQuery(Guid.Parse(request.ScormLearnerPackageId)), cancellationToken)
        };
        return response;
    }
}
