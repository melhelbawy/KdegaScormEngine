using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
public class AddLearnerToScormPackageCommand : IRequest<bool>
{
    public string LearnerId { get; set; } = null!;
    public string ScormPackageId { get; set; } = null!;
}

public class ProcessUserScormModuleCommandHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<AddLearnerToScormPackageCommand, bool>
{
    public ProcessUserScormModuleCommandHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<bool> Handle(AddLearnerToScormPackageCommand request, CancellationToken cancellationToken)
    {
        if (await IsUserEnrolled(request.LearnerId, request.ScormPackageId)) return false;

        await Context.LearnerScormPackages.AddAsync(new LearnerScormPackage
        {
            ScormPackageId = Guid.Parse(request.ScormPackageId),
            LearnerId = request.LearnerId,
            JoiningDate = DateTime.Now
        }, cancellationToken);

        await Mediator.Send(new InitLearnerCmiCoreCommand()
        {
            LearnerId = Guid.Parse(request.LearnerId),
            LearnerScormPackageId = Guid.Parse(request.ScormPackageId)
        }, cancellationToken);

        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }

    private async Task<bool> IsUserEnrolled(string learnerId, string scormContentId)
    {
        return await Mediator.Send(new IsLearnerJoinedPackageQuery()
        {
            LearnerId = learnerId,
            ScormPackageId = Guid.Parse(scormContentId)
        });
    }


}