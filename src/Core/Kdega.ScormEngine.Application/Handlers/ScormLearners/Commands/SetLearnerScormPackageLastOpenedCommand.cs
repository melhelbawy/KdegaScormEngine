using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
public class SetLearnerScormPackageLastOpenedCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
    public string ScormPackageId { get; set; } = null!;
}

public class SetLearnerScormPackageLastOpenedCommandHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<SetLearnerScormPackageLastOpenedCommand>
{
    public SetLearnerScormPackageLastOpenedCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(SetLearnerScormPackageLastOpenedCommand request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages.FindAsync(request.ScormPackageId, request.LearnerId);
        if (learnerScormPackage is not null)
        {
            learnerScormPackage.LastOpenedDate = DateTime.Now;
            await Context.SaveChangesAsync(cancellationToken);
        }
        ArgumentNullException.ThrowIfNull(learnerScormPackage);
    }
}
