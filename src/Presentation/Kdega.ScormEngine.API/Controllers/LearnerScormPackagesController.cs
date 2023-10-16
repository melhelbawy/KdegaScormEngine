using Kdega.ScormEngine.Application.Handlers.ScormLearners.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Kdega.ScormEngine.API.Controllers;

[ApiController]
public class LearnerScormPackagesController : BaseController
{
    public LearnerScormPackagesController(IMediator mediator) : base(mediator)
    {
    }


    [HttpPut]
    [SwaggerOperation("Add Scorm package to learner")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<JsonResult> AddLearnerToScormPackage(AddLearnerToScormPackageCommand command)
    {
        return Json(await Send(command));
    }

    [HttpGet("Joint-Validation")]
    [SwaggerOperation("Is learner joined an specific Scorm package")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> IsLearnerJoinedPackage(string learnerId, string scormPackageId)
    {
        return await Send(new IsLearnerJoinedPackageQuery()
        {
            LearnerId = learnerId,
            ScormPackageId = Guid.Parse(scormPackageId)
        });
    }
}

