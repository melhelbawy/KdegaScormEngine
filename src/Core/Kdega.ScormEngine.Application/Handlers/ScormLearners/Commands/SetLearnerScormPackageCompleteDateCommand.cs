using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
public class SetLearnerScormPackageCompleteDateCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
    public string ScormPackageId { get; set; } = null!;
    public DateTime CompleteDate { get; set; }
}

public class SetLearnerScormPackageCompleteDateCommandHandler : BaseHandler, IRequestHandler<SetLearnerScormPackageCompleteDateCommand>
{
    public SetLearnerScormPackageCompleteDateCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(SetLearnerScormPackageCompleteDateCommand request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages.FindAsync(request.ScormPackageId, request.LearnerId);
        if (learnerScormPackage is not null)
        {
            learnerScormPackage.CompletionDate = request.CompleteDate;
            await Context.SaveChangesAsync(cancellationToken);
        }
        ArgumentNullException.ThrowIfNull(learnerScormPackage);
    }
}