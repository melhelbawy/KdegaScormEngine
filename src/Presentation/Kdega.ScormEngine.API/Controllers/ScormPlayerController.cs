using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Commands.SetValues;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Queries;
using Kdega.ScormEngine.Application.Handlers.ScormLms.Queries.GetValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Kdega.ScormEngine.API.Controllers;

[ApiController]
[Route("Kdega")]
public class ScormPlayerController : BaseController
{
    public ScormPlayerController(IMediator mediator) : base(mediator)
    {
    }
    //PUT api/LMSSetValue
    [HttpPut("lms-initialize")]
    [SwaggerOperation("Kdega Scorm LmsInitialize -> Begins a communication session with the LMS.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsInitialize([FromBody] LmsRequest request)
    {
        return Json(await Send(new InitializeCommand(request)));
    }

    //PUT api/LMSFinish | 2004-> Terminate
    [HttpPut("lms-finish")]
    [SwaggerOperation("Kdega Scorm LmsFinish -> Ends a communication session with the LMS.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsFinish([FromBody] LmsRequest request)
    {
        return Json(await Send(new FinishCommand(request)));
    }

    //GET api/LMSGetValue | 2004 -> GetValue
    [HttpGet("lms-get-value")]
    [SwaggerOperation("Kdega Scorm LmsGetValue -> Retrieves a value from the LMS.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsGetValue([FromQuery] LmsRequest query)
    {
        return Json(await Send(new GetValueQuery(query)));
    }

    //POST api/LMSSetValue | 2004 -> SetValue
    [HttpPut("lms-set-value")]
    [SwaggerOperation("Kdega Scorm LmsSetValue -> Saves a value to the LMS.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsSetValue([FromBody] LmsRequest request)
    {
        return Json(await Send(new SetValueCommand(request)));
    }

    //PUT api/LMSCommit | 2004 -> Commit
    [HttpPut("lms-commit")]
    [SwaggerOperation("Kdega Scorm LmsCommit -> Indicates to the LMS that all data should be persisted (not required).")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsCommit([FromBody] LmsRequest request)
    {
        return Json(await Send(new CommitCommand(request)));
    }

    //GET api/LMSGetLastError | 2004 -> GetLastError
    [HttpGet("lms-get-last-error")]
    [SwaggerOperation("Kdega Scorm LmsGetLastError -> Returns the error code that resulted from the last API call.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsGetLastError([FromQuery] LmsRequest query)
    {
        return Json(await Send(new GetLastErrorQuery()));
    }

    //GET api/LMSGetErrorString | 2004 -> GetErrorString
    [HttpGet("lms-get-error-string")]
    [SwaggerOperation("Kdega Scorm LmsGetErrorString -> Returns a short string describing the specified error code.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsGetErrorString([FromQuery] LmsRequest query)
    {
        return Json(await Send(new GetErrorStringQuery()));
    }

    //GET api/LMSGetDiagnostic | 2004 -> GetDiagnostic
    [HttpGet("lms-get-diagnostic")]
    [SwaggerOperation("Kdega Scorm LmsGetDiagnostic -> Returns detailed information about the last error that occurred.")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsRequest))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> LmsGetDiagnostic([FromQuery] LmsRequest query)
    {
        return Json(await Send(new GetDiagnosticQuery()));
    }
}
