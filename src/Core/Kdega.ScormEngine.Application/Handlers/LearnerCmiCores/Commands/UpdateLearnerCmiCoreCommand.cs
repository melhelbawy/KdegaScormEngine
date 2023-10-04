using Kdega.ScormEngine.Application.Behavior;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
public class UpdateLearnerCmiCoreCommand : IRequest
{
    public string LearnerCmiCoreId { get; set; } = null!;
    public string Entry { get; set; } = null!;
    public string LessonStatus { get; set; } = null!;
    public string TotalTime { get; set; } = null!;
}

public class UpdateLearnerCmiCoreCommandHandler : BaseHandler, IRequestHandler<UpdateLearnerCmiCoreCommand>
{
    public UpdateLearnerCmiCoreCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(UpdateLearnerCmiCoreCommand request, CancellationToken cancellationToken)
    {
        var learnerCmiCore = await Context.CmiCores.FindAsync(request.LearnerCmiCoreId);

        Check.NotNull(learnerCmiCore, nameof(learnerCmiCore));

        learnerCmiCore.Entry = request.Entry;
        learnerCmiCore.LessonStatus = request.LessonStatus;
        learnerCmiCore.TotalTime = request.TotalTime;
        await Context.SaveChangesAsync(cancellationToken);

    }
}
