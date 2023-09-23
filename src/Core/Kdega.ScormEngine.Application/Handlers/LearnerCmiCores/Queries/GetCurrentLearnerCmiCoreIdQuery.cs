using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

public class GetCurrentLearnerCmiCoreIdQuery : IRequest<string>
{
    public Guid ScormLearnerPackageId { get; set; }
}

public class GetCurrentLearnerCmiCoreIdQueryHandler : BaseHandler<CmiCore>, IRequestHandler<GetCurrentLearnerCmiCoreIdQuery, string>
{
    public GetCurrentLearnerCmiCoreIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(GetCurrentLearnerCmiCoreIdQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FirstOrDefaultAsync(x => x.LearnerScormPackageId == request.ScormLearnerPackageId, CancellationToken.None);

        Check.NotNull(cmiCore, nameof(cmiCore));

        return cmiCore.Id.ToString();
    }
}
