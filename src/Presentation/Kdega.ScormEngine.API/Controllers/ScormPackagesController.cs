using Kdega.ScormEngine.Application.Handlers.ScormContents.Commands.Uploading;
using Kdega.ScormEngine.Application.Handlers.ScormContents.Models;
using Kdega.ScormEngine.Application.Handlers.ScormContents.Queries;
using Kdega.ScormEngine.Infrastructure.Storage.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Kdega.ScormEngine.API.Controllers;

[ApiController]
public class ScormPackagesController : BaseController
{
    public ScormPackagesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    [SwaggerOperation("Kdega Scorm storage uploader endpoint")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes("multipart/form-data")]
    public async Task<JsonResult> UploadScormPackage([FromForm] UploadScormPackageCommand command)
    {
        return Json(await Send(command));
    }

    [HttpPut("manifest")]
    [SwaggerOperation("Kdega Scorm Scorm package manifest parsing endpoint")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ScormPackageManifestDto))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> ParseManifestCommand(ParseManifestCommand command)
    {
        return Json(await Send(command));
    }

    [HttpGet("files/{path}")]
    [SwaggerOperation("Kdega Scorm Scorm package manifest parsing endpoint")]
    [Produces(MediaTypeNames.Application.Octet)]
    public async Task<IActionResult> GetObjectStreamAsync([FromRoute] string path)
    {
        var stream = await Mediator.Send(new GetContentStreamQuery() { Path = path });
        var filename = path.GetPathSegments().Last();

        return File(stream, filename.GetObjectType());
    }
}

