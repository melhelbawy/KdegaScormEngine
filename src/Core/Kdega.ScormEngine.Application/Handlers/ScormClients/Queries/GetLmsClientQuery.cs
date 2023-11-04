using Kdega.ScormEngine.Application.Handlers.LmsSessions.Queries;
using Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormClients.Queries;
public class GetLmsClientQuery : IRequest<LmsClientParamsDto>
{
    public GetLmsClientQuery(string scormLearnerPackageId)
    {
        ScormLearnerPackageId = scormLearnerPackageId;
    }

    public string ScormLearnerPackageId { get; set; }
}

public class GetLmsClientQueryHandler : BaseHandler, IRequestHandler<GetLmsClientQuery, LmsClientParamsDto>
{
    public GetLmsClientQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsClientParamsDto> Handle(GetLmsClientQuery request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .Include(x => x.ScormPackage)
            .Include(x => x.CmiCores)
            .Where(x => x.Id == Guid.Parse(request.ScormLearnerPackageId))
            .FirstOrDefaultAsync(cancellationToken);

        var coreId = learnerScormPackage?.CmiCores
            .First(x => x.LearnerScormPackageId == Guid.Parse(request.ScormLearnerPackageId)).Id.ToString();

        var sessionId = await Mediator.Send(
            new GetCurrentLearnerSessionIdQuery
            {
                LearnerId = learnerScormPackage!.LearnerId!,
                CmiCoreId = coreId
            },

                cancellationToken);
        var response = new LmsClientParamsDto
        {
            SessionId = sessionId,
            ScoIdentifier = learnerScormPackage.ScormPackage.DefaultOrganizationIdentifier,
            ScormLearnerPackageId = request.ScormLearnerPackageId,
            FolderPath = learnerScormPackage.ScormPackage.PackageFolderPath,
            IndexPath = learnerScormPackage.ScormPackage.IndexPath,
            ScormVersion = learnerScormPackage.ScormPackage.ScormVersion,
            DateCreated = learnerScormPackage.CreatedOn.ToString("G"),
        };
        return response;
    }
}

