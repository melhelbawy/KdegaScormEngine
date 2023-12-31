﻿using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;

public class GetCurrentLearnerCmiCoreIdQuery : IRequest<string?>
{
    public Guid ScormLearnerPackageId { get; set; }

    public GetCurrentLearnerCmiCoreIdQuery(string scormLearnerPackageId)
    {
        ScormLearnerPackageId = Guid.Parse(scormLearnerPackageId);
    }
}

public class GetCurrentLearnerCmiCoreIdQueryHandler : BaseHandler, IRequestHandler<GetCurrentLearnerCmiCoreIdQuery, string?>
{
    public GetCurrentLearnerCmiCoreIdQueryHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string?> Handle(GetCurrentLearnerCmiCoreIdQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FirstOrDefaultAsync(x => x.LearnerScormPackageId == request.ScormLearnerPackageId, CancellationToken.None);

        return cmiCore?.Id.ToString() ?? null;
    }
}
