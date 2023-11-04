using Kdega.ScormEngine.Domain.Constants.ScormLms;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;
using System.Xml;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
public class InitLearnerCmiCoreCommand : IRequest<string>
{
    public Guid LearnerScormPackageId { get; set; }
    public Guid LearnerId { get; set; }
    public string LearnerName { get; set; } = string.Empty;
}

public class InitLearnerCmiCoreCommandHandler : BaseHandler, IRequestHandler<InitLearnerCmiCoreCommand, string>
{
    public InitLearnerCmiCoreCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<string> Handle(InitLearnerCmiCoreCommand request, CancellationToken cancellationToken)
    {
        var learnerCmiCore = new CmiCore
        {
            LearnerId = request.LearnerId.ToString(),
            StudentName = request.LearnerName,
            LessonStatus = ScormCmiCore.CmiCoreLessonStatus.NotAttempted,
            Credit = ScormCmiCore.CmiCoreCredits.Credit,
            LessonMode = ScormCmiCore.CmiCoreLessonMode.Normal,
            Entry = ScormCmiCore.CmiCoreEntry.AbInitio,
            LearnerScormPackageId = request.LearnerScormPackageId,
            TotalTime = XmlConvert.ToString(TimeSpan.Zero)
        };
        await Context.CmiCores.AddAsync(learnerCmiCore, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return learnerCmiCore.Id.ToString();
    }
}