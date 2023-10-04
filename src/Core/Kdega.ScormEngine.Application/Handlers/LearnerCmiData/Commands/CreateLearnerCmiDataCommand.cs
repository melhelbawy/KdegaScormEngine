using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Mapster;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiData.Commands;
public class CreateLearnerCmiDataCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
    public string? LearnerName { get; set; } = null!;
    public string ScoIdentifier { get; set; } = "default";
    public string? SuspendData { get; set; }
    public string? LunchData { get; set; }
    public string? MaxTimeAllowed { get; set; }
    public string? TimeLimitAction { get; set; }
    public decimal? MasteryScore { get; set; }
}

public class CreateLearnerCmiDataCommandHandler : BaseHandler, IRequestHandler<CreateLearnerCmiDataCommand>
{
    public CreateLearnerCmiDataCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task Handle(CreateLearnerCmiDataCommand request, CancellationToken cancellationToken)
    {
        await Context.CmiData.AddAsync(request.Adapt<CmiData>(), cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}