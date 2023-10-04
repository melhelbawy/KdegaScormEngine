using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;

[ScormMediatorComponent("cmi.core.lesson_status")]
public class SetLearnScormStatusCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetLearnScormStatusCommandHandler : BaseHandler, IRequestHandler<SetLearnScormStatusCommand, LmsRequest>
{
    public SetLearnScormStatusCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetLearnScormStatusCommand request, CancellationToken cancellationToken)
    {
        var learnerScormPackage = await Context.LearnerScormPackages
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.LearnerScormPackageId!), cancellationToken);

        switch (request.LmsRequest.DataValue?.ToLower())
        {
            case ScormCmiCore.CmiCoreLessonStatus.Completed:
                learnerScormPackage!.CompletionDate = DateTime.Now;
                await Context.SaveChangesAsync(cancellationToken);
                break;
            case ScormCmiCore.CmiCoreLessonStatus.Passed:
                learnerScormPackage!.DatePassed = DateTime.Now;
                await Context.SaveChangesAsync(cancellationToken);
                break;
            default:
                request.LmsRequest.InitCode405();
                break;
        }

        return request.LmsRequest;
    }
}
