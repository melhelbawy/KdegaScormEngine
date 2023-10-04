using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Queries;

public class GetCurrentLearnerCmiDataIdQuery : IRequest<string>
{
    public string LearnerId { get; set; } = null!;
    public Guid CmiCoreId { get; set; }
    public Guid ScormPackageId { get; set; }
}

public class GetCurrentLearnerCmiDataIdQueryHandler : BaseHandler, IRequestHandler<GetCurrentLearnerCmiDataIdQuery, string>
{
    public GetCurrentLearnerCmiDataIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(GetCurrentLearnerCmiDataIdQuery request, CancellationToken cancellationToken)
    {
        var cmiData = await Context.CmiData.FirstOrDefaultAsync(x => x.LearnerId == request.LearnerId
                                                                     && x.CmiCoreId == request.CmiCoreId
                                                                     && x.ScormContentId == request.ScormPackageId, CancellationToken.None);

        Check.NotNull(cmiData, nameof(CmiData));
        return cmiData.Id.ToString();
    }
}
