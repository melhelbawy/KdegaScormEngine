using Kdega.ScormEngine.Application.Handlers.ScormLearners.Models;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;
public class GetLearnerPackageLaunchQuery : IRequest<PackageLaunchParams>
{
    public Guid Id { get; set; }

    public GetLearnerPackageLaunchQuery(Guid id)
    {
        Id = id;
    }
}

public class GetLearnerPackageLaunchQueryHandler : BaseHandler, IRequestHandler<GetLearnerPackageLaunchQuery, PackageLaunchParams>
{
    public GetLearnerPackageLaunchQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<PackageLaunchParams> Handle(GetLearnerPackageLaunchQuery request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .Include(x => x.CmiCores)
            .Include(x => x.ScormPackage)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(learnerScormPackage, nameof(LearnerScormPackage));

        return new PackageLaunchParams
        {
            Id = learnerScormPackage.Id,
            LearnerId = learnerScormPackage.LearnerId,
            CmiCoreId = learnerScormPackage.CmiCores.Single(x => x.LearnerScormPackageId == request.Id).Id,
            FolderPath = learnerScormPackage.ScormPackage.PackageFolderPath,
            IndexPath = learnerScormPackage.ScormPackage.IndexPath,
            DefaultOrganizationIdentifier = learnerScormPackage.ScormPackage.DefaultOrganizationIdentifier,
            ScormVersion = learnerScormPackage.ScormPackage.ScormVersion
        };
    }
}