using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;

#region Scorm Versions 1.1 and 1.2
[ScormMediatorComponent("cmi.core.lesson_location", ScormApiMethod.Set)]
public class SetCmiCoreLessonLocationCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCoreLessonLocationCommandHandler : BaseHandler, IRequestHandler<SetCmiCoreLessonLocationCommand, LmsRequest>
{
    public SetCmiCoreLessonLocationCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiCoreLessonLocationCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiString255(request.LmsRequest.DataValue!))
        {
            cmiCore!.LessonLocation = request.LmsRequest.DataValue;
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
#endregion

#region Scorm Versions 2004 2nd, 3rd, 4th Edition
[ScormMediatorComponent("cmi.location", ScormApiMethod.Set)]
public class SetCmiCoreLocationCommand : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class SetCmiCoreLocationCommandHandler : BaseHandler, IRequestHandler<SetCmiCoreLocationCommand, LmsRequest>
{
    public SetCmiCoreLocationCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(SetCmiCoreLocationCommand request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.LmsRequest.CoreId), cancellationToken);

        if (ScormDataValidatorHelper.IsCmiString1000(request.LmsRequest.DataValue!))
        {
            cmiCore!.LessonLocation = request.LmsRequest.DataValue;
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            request.LmsRequest.InitCode405();

        return request.LmsRequest;
    }
}
#endregion
