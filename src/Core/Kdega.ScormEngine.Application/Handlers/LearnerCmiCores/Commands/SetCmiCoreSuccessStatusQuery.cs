using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
[ScormMediatorComponent("cmi.success_status", ScormApiMethod.Set)]
public class SetCmiCoreSuccessStatusQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCoreSuccessStatusQueryHandler : BaseHandler, IRequestHandler<SetCmiCoreSuccessStatusQuery, LmsRequest>
{
    public SetCmiCoreSuccessStatusQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(SetCmiCoreSuccessStatusQuery request, CancellationToken cancellationToken)
    {
        if (ScormDataValidatorHelper.IsCmiVocabulary("status", request.LmsRequest.DataValue!))
        {
            var cmiCore = await Context.CmiCores.FindAsync(request.LmsRequest.CoreId);
            cmiCore!.LessonStatus = request.LmsRequest.DataValue;
            cmiCore.SuccessStatus = request.LmsRequest.DataValue;

            var learnerModule = await Context.LearnerScormPackages
                .FirstAsync(x => x.Id == Guid.Parse(request.LmsRequest.LearnerScormPackageId!), cancellationToken);

            if (request.LmsRequest.DataValue == ScormCmiCore.CmiCoreSuccessStatus.Passed)
            {
                learnerModule.DatePassed = DateTime.Now;
            }

            await Context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            request.LmsRequest.InitCode405();
        }
        return request.LmsRequest;
    }


}