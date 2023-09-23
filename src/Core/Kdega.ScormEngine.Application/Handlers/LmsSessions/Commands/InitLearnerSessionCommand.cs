using Kdega.ScormEngine.Application.Constants;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
public class InitLearnerSessionCommand : IRequest<string>
{
    public string CmiCoreId { get; set; } = null!;
    public string LearnerId { get; set; } = null!;
    public string ScoIdentifier { get; set; } = "default";
}

public class InitLearnerSessionCommandHandler : BaseHandler<ScormSession>, IRequestHandler<InitLearnerSessionCommand, string>
{
    public InitLearnerSessionCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(InitLearnerSessionCommand request, CancellationToken cancellationToken)
    {
        //var currentScoIdentifier = await _manifestRepository.FindOneAsync(x => x.Id.ToString() == request.ScormContentId);

        var learnerSession = new ScormSession()
        {
            SessionId = LmsClient.SessionId,
            CmiCoreId = Guid.Parse(request.CmiCoreId),
            LearnerId = request.LearnerId,
            ScoIdentifier = request.ScoIdentifier,
            StartDatetime = DateTime.Now
        };

        await Context.ScormSessions.AddAsync(learnerSession, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return learnerSession.Id.ToString();
    }
}
