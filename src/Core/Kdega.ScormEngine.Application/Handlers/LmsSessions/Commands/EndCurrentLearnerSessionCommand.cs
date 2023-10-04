using Kdega.ScormEngine.Application.Behavior;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
public class EndCurrentLearnerSessionCommand : IRequest<bool>
{
    public string SessionId { get; set; } = null!;
    public string LearnerId { get; set; } = null!;
}

public class EndCurrentLearnerSessionCommandHandler : BaseHandler, IRequestHandler<EndCurrentLearnerSessionCommand, bool>
{
    public EndCurrentLearnerSessionCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<bool> Handle(EndCurrentLearnerSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await Context.ScormSessions
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.SessionId), cancellationToken);

        Check.NotNull(session, nameof(session));

        session.EndDatetime = DateTime.Now;
        Context.ScormSessions.Update(session);

        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }
}