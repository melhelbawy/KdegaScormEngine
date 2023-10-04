using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
using Kdega.ScormEngine.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Commands;
public class FinishCommand : IRequest<LmsRequest>
{
    public FinishCommand(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }

}


public class FinishCommandHandler : BaseHandler, IRequestHandler<FinishCommand, LmsRequest>
{
    public FinishCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<LmsRequest> Handle(FinishCommand request, CancellationToken cancellationToken)
    {
        var response = new LmsRequest();
        if (request?.Request is null)
        {
            response.ErrorCode = "201";
            response.ErrorString = "Invalid or incomplete data, can't initialize";
            response.ReturnValue = "false";
            return response;
        }

        if (!string.IsNullOrEmpty(request.Request.SessionId))
        {
            await Mediator.Send(new EndCurrentLearnerSessionCommand()
            {
                SessionId = request.Request.SessionId,
                LearnerId = request.Request.LearnerId!
            }, cancellationToken);
        }

        if (!string.IsNullOrEmpty(request.Request.CoreId))
        {
            var cmiCore = await Context.CmiCores
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Request.CoreId), cancellationToken);
            Check.NotNull(cmiCore, nameof(cmiCore));

            var totalTime = ScormDataValidatorHelper.AddCmiTime(cmiCore.SessionTime!, cmiCore.TotalTime!);

            if (cmiCore.Exit != null && cmiCore.Exit.Equals("suspend", StringComparison.CurrentCultureIgnoreCase))
            {
                cmiCore.Entry = "resume";
            }

            if (cmiCore.LessonStatus != null && (cmiCore.LessonStatus.Equals("not attempted", StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrEmpty(cmiCore.LessonStatus)))
            {
                cmiCore.LessonStatus = "completed";
            }
            else
            {
                cmiCore.LessonStatus = cmiCore.LessonStatus;
            }

            if (totalTime == "false")
            {
                totalTime = cmiCore.TotalTime;
            }

            Context.CmiCores.Update(cmiCore);

            await Context.SaveChangesAsync(cancellationToken);

            response.ErrorCode = "0";
            response.ReturnValue = "true";
            response.ErrorString = "";
            return response;
        }

        response.ErrorCode = "301";
        response.ReturnValue = "false";
        response.ErrorString = "No cmi_core record for this session";
        return response;
    }
}

