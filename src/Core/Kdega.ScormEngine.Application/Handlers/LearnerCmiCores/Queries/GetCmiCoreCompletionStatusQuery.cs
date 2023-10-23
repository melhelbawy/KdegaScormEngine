using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
[ScormMediatorComponent("cmi.completion_status", ScormApiMethod.Get)]
public class GetCmiCoreCompletionStatusQuery : IRequest<LmsRequest>
{
    public LmsRequest LmsRequest { get; set; } = null!;
}

public class GetCmiCoreCompletionStatusQueryHandler : BaseHandler, IRequestHandler<GetCmiCoreCompletionStatusQuery, LmsRequest>
{
    public GetCmiCoreCompletionStatusQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<LmsRequest> Handle(GetCmiCoreCompletionStatusQuery request, CancellationToken cancellationToken)
    {
        var cmiCore = await Context.CmiCores.FindAsync(request.LmsRequest.CoreId);
        request.LmsRequest.InitCode0Success(cmiCore.CompletionStatus.IsNullOrEmpty()
            ? ScormCmiCore.CmiCoreCompletionStatus.Unknown
            : cmiCore.CompletionStatus!);

        return request.LmsRequest;
    }


}