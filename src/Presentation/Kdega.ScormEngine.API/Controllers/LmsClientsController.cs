using Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
using Kdega.ScormEngine.Application.Handlers.ScormClients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Kdega.ScormEngine.API.Controllers;
[ApiController]

public class LmsClientsController : BaseController
{
    public LmsClientsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{learnerScormPackageId}/learner-package")]
    [SwaggerOperation("Is learner joined an specific Scorm package")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LmsClientParamsDto))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> IsLearnerJoinedPackage(string learnerScormPackageId)
    {
        return await Send(new GetLmsClientQuery(learnerScormPackageId));
    }
}
