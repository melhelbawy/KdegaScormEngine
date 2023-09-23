using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;

public class GetCurrentLearnerPackageIdQuery : IRequest<string>
{
    public string ScormContentId { get; set; } = null!;
    public string LearnerId { get; set; } = null!;
}

public class GetCurrentLearnerModuleIdQueryHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<GetCurrentLearnerPackageIdQuery, string>
{
    public GetCurrentLearnerModuleIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(GetCurrentLearnerPackageIdQuery request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .FirstOrDefaultAsync(x => x.ScormPackageId == Guid.Parse(request.ScormContentId)
                                      && x.LearnerId == request.LearnerId, cancellationToken);
        Check.NotNull(learnerScormPackage, nameof(learnerScormPackage));

        return learnerScormPackage.Id.ToString();
    }
}