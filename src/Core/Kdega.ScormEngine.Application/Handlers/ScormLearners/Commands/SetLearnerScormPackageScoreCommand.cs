using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
public class SetLearnerScormPackageScoreCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
    public string ScormPackageId { get; set; } = null!;
    public decimal Score { get; set; }
}

public class SetLearnerScormPackageScoreCommandHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<SetLearnerScormPackageScoreCommand>
{
    public SetLearnerScormPackageScoreCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(SetLearnerScormPackageScoreCommand request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages.FindAsync(request.ScormPackageId, request.LearnerId);
        if (learnerScormPackage is not null)
        {
            learnerScormPackage.Score = request.Score;
            await Context.SaveChangesAsync(cancellationToken);
        }
        ArgumentNullException.ThrowIfNull(learnerScormPackage);
    }
}
