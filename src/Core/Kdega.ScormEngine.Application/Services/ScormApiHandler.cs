using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Application.Interfaces;
using MediatR;

namespace Kdega.ScormEngine.Application.Services;
public class ScormApiHandler : IScormApiHandler
{
    private readonly IMediator _mediator;

    public ScormApiHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<LmsRequest> SetValue(string scormApiKey, LmsRequest request)
    {
        var keyCommands = ScormApiHelper.GetScormKeyCommands(scormApiKey, request);

        foreach (var keyCommand in keyCommands)
        {
            await Handle(keyCommand);
        }

        return request;
    }

    public async Task<LmsRequest> GetValue(string scormApiKey, LmsRequest request)
    {
        throw new NotImplementedException();
    }


    private async Task Handle(IRequest<LmsRequest> request) => await _mediator.Send(request);
}
