﻿using Kdega.ScormEngine.Application.Constants;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
public class InitLearnerCmiCoreCommand : IRequest<string>
{
    public Guid LearnerScormPackageId { get; set; }
    public Guid LearnerId { get; set; }
}

public class InitLearnerCmiCoreCommandHandler : BaseHandler<CmiCore>, IRequestHandler<InitLearnerCmiCoreCommand, string>
{
    public InitLearnerCmiCoreCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(InitLearnerCmiCoreCommand request, CancellationToken cancellationToken)
    {
        var learnerCmiCore = new CmiCore
        {
            LearnerId = request.LearnerId.ToString(),
            LessonStatus = ScormCmiCore.CmiCoreLessonStatus.NotAttempted,
            Credit = ScormCmiCore.CmiCoreCredits.Credit,
            LessonMode = ScormCmiCore.CmiCoreLessonMode.Normal,
            Entry = ScormCmiCore.CmiCoreEntry.AbInitio,
            LearnerScormPackageId = request.LearnerScormPackageId,
        };
        await Context.CmiCores.AddAsync(learnerCmiCore, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return learnerCmiCore.Id.ToString();
    }
}