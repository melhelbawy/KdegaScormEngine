using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Queries.GetValues;
public class GetValueQuery : IRequest<LmsRequest>
{
    public GetValueQuery(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }
}


public class GetValueQueryHandler : BaseHandler, IRequestHandler<GetValueQuery, LmsRequest>
{
    private readonly IScormMediator _scormMediator;


    public GetValueQueryHandler(IServiceProvider provider) : base(provider)
    {
        _scormMediator = provider.GetCustomRequiredService<IScormMediator>();
    }

    public async Task<LmsRequest> Handle(GetValueQuery request, CancellationToken cancellationToken)
    {
        if (request.Request.DataValue is not null && request.Request.DataValue == "NaN")
            request.Request.DataValue = string.Empty;

        if (string.IsNullOrEmpty(request.Request.DataValue))
        {
            request.Request.InitCode201();
            return request.Request;
        }

        if (!await IsValidCoreId(request.Request.CoreId))
            request.Request.InitCode301();
        else if (ScormDataValidatorHelper.IsWriteOnly(request.Request.DataItem))
            request.Request.InitCode404();
        else if (ScormDataValidatorHelper.IsKeyword(request.Request.DataItem))
            request.Request.InitCode402();
        else
            await _scormMediator.Handle(request.Request.DataItem, request.Request, ScormApiMethod.Get);

        return request.Request;
    }

    private async Task<bool> IsValidCoreId(string coreId)
    {
        if (Guid.TryParse(coreId, out var result))
            return await Context.CmiCores.AnyAsync(x => x.Id == result);
        return false;
    }
}
