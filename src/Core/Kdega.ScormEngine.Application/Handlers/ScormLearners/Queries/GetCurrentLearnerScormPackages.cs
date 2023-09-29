using Kdega.ScormEngine.Application.Handlers.ScormLearners.Models;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;
public class GetCurrentLearnerScormPackages : IRequest<List<LearnerScormPackageDto>>
{
    public string LearnerId { get; set; } = null!;

}

public class GetCurrentLearnerScormPackagesHandler : BaseHandler<LearnerScormPackage>, IRequestHandler<GetCurrentLearnerScormPackages,
        List<LearnerScormPackageDto>>
{
    public GetCurrentLearnerScormPackagesHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<List<LearnerScormPackageDto>> Handle(GetCurrentLearnerScormPackages request, CancellationToken cancellationToken)
    {
        var learnerScorms = await Context.LearnerScormPackages.Where(x => x.LearnerId == request.LearnerId)
            .ProjectToType<LearnerScormPackageDto>()
            .OrderByDescending(x => x.JoiningDate)
            .ToListAsync(cancellationToken);
        return learnerScorms;
    }
}