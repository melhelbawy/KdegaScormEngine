﻿using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Helpers;
using Kdega.ScormEngine.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Commands.SetValues;

public class SetValueCommand : IRequest<LmsRequest>
{
    public SetValueCommand(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }
}

public class SetValueCommandHandler : BaseHandler, IRequestHandler<SetValueCommand, LmsRequest>
{
    private readonly IScormMediator _scormMediator;


    public SetValueCommandHandler(IServiceProvider provider) : base(provider)
    {
        _scormMediator = provider.GetCustomRequiredService<IScormMediator>();
    }

    public async Task<LmsRequest> Handle(SetValueCommand request, CancellationToken cancellationToken)
    {
        request.Request.ErrorCode = "0";
        request.Request.ReturnValue = "true";
        request.Request.ErrorString = string.Empty;

        if (request.Request.DataValue is not null && request.Request.DataValue == "NaN")
            request.Request.DataValue = string.Empty;

        if (!await IsValidCoreId(request.Request.CoreId))
            request.Request.InitCode301();
        else if (ScormDataValidatorHelper.IsReadOnly(request.Request.DataItem))
            request.Request.InitCode403();
        else if (ScormDataValidatorHelper.IsKeyword(request.Request.DataItem))
            request.Request.InitCode402();
        else
            await _scormMediator.Handle(request.Request.DataItem, request.Request);

        return request.Request;
    }

    private async Task<bool> IsValidCoreId(string coreId)
    {
        if (Guid.TryParse(coreId, out var result))
            return await Context.CmiCores.AnyAsync(x => x.Id == result);
        return false;
    }
}
