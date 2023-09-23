using Kdega.ScormEngine.Application.Common.Models;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Commands;
public class CommitCommand : IRequest<LmsRequest>
{
    public CommitCommand(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }
}

public class CommitCommandHandler : IRequestHandler<CommitCommand, LmsRequest>
{
    public Task<LmsRequest> Handle(CommitCommand request, CancellationToken cancellationToken)
    {
        // this is a NOOP since we commit every time.
        request.Request.ErrorCode = "0";
        request.Request.ReturnValue = "true";
        request.Request.ErrorString = "";

        return Task.FromResult(request.Request);
    }
}