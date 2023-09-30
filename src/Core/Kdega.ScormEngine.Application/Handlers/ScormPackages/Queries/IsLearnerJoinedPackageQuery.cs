using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
public class IsLearnerJoinedPackageQuery : IRequest<bool>
{
    public Guid ScormPackageId { get; set; }
    public string LearnerId { get; set; } = null!;
}

public class IsLearnerJoinedPackageQueryHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<IsLearnerJoinedPackageQuery, bool>
{
    public IsLearnerJoinedPackageQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<bool> Handle(IsLearnerJoinedPackageQuery request, CancellationToken cancellationToken)
    {

        return await Context.LearnerScormPackages
            .AnyAsync(x => x.LearnerId == request.LearnerId
                           && x.ScormPackageId == request.ScormPackageId, cancellationToken);
    }
}
