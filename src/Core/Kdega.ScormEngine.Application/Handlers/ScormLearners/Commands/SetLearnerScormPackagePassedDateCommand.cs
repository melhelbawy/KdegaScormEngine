using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
public class SetLearnerScormPackagePassedDateCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
    public string ScormPackageId { get; set; } = null!;
    public DateTime? DatePassed { get; set; }
}

public class SetLearnerScormPackagePassedDateCommandHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<SetLearnerScormPackagePassedDateCommand>
{
    public SetLearnerScormPackagePassedDateCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(SetLearnerScormPackagePassedDateCommand request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages.FindAsync(request.ScormPackageId, request.LearnerId);
        if (learnerScormPackage is not null)
        {
            learnerScormPackage.DatePassed = request.DatePassed ?? DateTime.Now;
            await Context.SaveChangesAsync(cancellationToken);
        }
        ArgumentNullException.ThrowIfNull(learnerScormPackage);
    }
}
