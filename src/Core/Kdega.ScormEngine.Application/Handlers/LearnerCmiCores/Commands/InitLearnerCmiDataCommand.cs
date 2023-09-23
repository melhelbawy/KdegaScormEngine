using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
public class InitLearnerCmiDataCommand : IRequest
{
    public string LearnerId { get; set; } = null!;
}


