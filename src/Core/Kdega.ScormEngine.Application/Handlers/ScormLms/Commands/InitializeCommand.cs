using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Commands;
public class InitializeCommand : IRequest<LmsRequest>
{
    public InitializeCommand(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest? Request { get; set; }
}

public class InitializeCommandHandler : BaseHandler, IRequestHandler<InitializeCommand, LmsRequest>
{
    public InitializeCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(InitializeCommand? request, CancellationToken cancellationToken)
    {
        if (request!.Request is null)
        {
            request.Request!.ErrorCode = "201";
            request.Request.ErrorString = "Invalid or incomplete data, can't initialize";
            request.Request.ReturnValue = "false";
            return request.Request;
        }

        var cmiCoreId = await Mediator.Send(new GetCurrentLearnerCmiCoreIdQuery(Guid.Parse(request.Request.LearnerScormPackageId!))
            , cancellationToken);

        if (string.IsNullOrEmpty(cmiCoreId))
        {
            await Context.CmiData.AddAsync(new CmiData()
            {
                LearnerId = request.Request.LearnerId,
                ScoIdentifier = request.Request.ScoIdentifier,
                ScormContentId = Guid.Parse(request.Request.LearnerScormPackageId!),
                LaunchData = string.Empty,
                MaxTimeAllowed = string.Empty,
                TimeLimitAction = string.Empty,
                MasteryScore = null
            }, cancellationToken);
        }
        request.Request.ErrorCode = "0";
        request.Request.ReturnValue = "true";
        request.Request.ErrorString = "";
        request.Request.CoreId = cmiCoreId;
        request.Request.LearnerScormPackageId = request.Request.LearnerScormPackageId;
        request.Request.LearnerId = request.Request.LearnerId;

        return request.Request;
    }
}

