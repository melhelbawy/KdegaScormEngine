using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Commands.SetValues;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Queries.GetValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Kdega.ScormEngine.API.Controllers;

[ApiController]
public class LmsController : BaseController
{
    public LmsController(IMediator mediator) : base(mediator)
    {
    }


    [HttpPut("set-value")]
    [SwaggerOperation("Testing SET Value Scorm Data Model APIs")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> AddLearnerToScormPackage([FromBody] LmsRequest request)
    {
        return Json(await Send(new SetValueCommand(request)));
    }

    [HttpGet("get-value")]
    [SwaggerOperation("Testing GET ValueScorm Data Model APIs")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> IsLearnerJoinedPackage([FromQuery] LmsRequest query)
    {
        return await Send(new GetValueQuery(query));
    }
}

