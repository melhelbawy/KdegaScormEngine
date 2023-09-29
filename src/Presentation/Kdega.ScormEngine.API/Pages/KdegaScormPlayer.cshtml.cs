using Kdega.ScormEngine.Application.Constants;
using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
using Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
using Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kdega.ScormEngine.API.Pages;

public class KdegaScormPlayerModel : PageModel
{
    private readonly IMediator _mediator;

    public string? ScormPackageId { get; set; }
    public string? ScormLearnerPackageId { get; set; }
    public string? LearnerId { get; set; }
    public string? LaunchParameters { get; set; }
    public string? IFrameSrc { get; set; }


    public KdegaScormPlayerModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void OnGet()
    {
        if (string.IsNullOrEmpty(Request.Query["path"]))
            throw new ArgumentException("you must provide Scorm Index file path");

        LearnerId = Request.Query["learnerId"]!;
        var scoLaunch = GetCourseInformation(LearnerId);
        LaunchParameters = SetupJavaScript(scoLaunch);
        var stream = _mediator.Send(new GetContentStreamQuery { Path = Request.Query["path"]! });
    }
    private string SetupJavaScript(LmsClientDto scoLaunch)
    {
        return $@"var lmsClient = lmsClient || {{}};
                          lmsClient.sessionId = '{scoLaunch.SessionId}';
                          lmsClient.learnerId = '{scoLaunch.LearnerId}';
                          lmsClient.coreId = '{scoLaunch.CoreId}';
                          lmsClient.learnerPackageId = '{ScormLearnerPackageId}';
                          lmsClient.sco_identifier = 'default'; 
                          lmsClient.scorm_package_id = '{scoLaunch.ScormPackageId}';
                          lmsClient.scoAddress = '{scoLaunch.ScoAddress}';
                          lmsClient.scoFrameClientID = '{LmsClient.ScoFrameClientId}';
                          lmsClient.divDebugID = '{LmsClient.BDebug}';
                          lmsClient.bDebug = {LmsClient.BDebug};
                          lmsClient.DateCreated = '{DateTime.Today}'; ";

    }

    private LmsClientDto GetCourseInformation(string userId)
    {
        var scoLaunch = new LmsClientDto
        {
            ScormPackageId = ScormPackageId,
            LearnerId = userId,
            ScoAddress = ""
        };


        var coreId = GetPackageLearnerScoCoreId();
        scoLaunch.CoreId = coreId;
        scoLaunch.SessionId = GetScoSessionId(coreId, LearnerId!);
        return scoLaunch;
    }

    private string GetScoSessionId(string coreId, string learnerId) =>
        _mediator.Send(new InitLearnerSessionCommand()
        {
            CmiCoreId = coreId,
            ScoIdentifier = "default",
            LearnerId = learnerId,
        }).Result;

    private string GetPackageLearnerScoCoreId() =>
        _mediator.Send(new GetCurrentLearnerCmiCoreIdQuery(Guid.Parse(ScormLearnerPackageId!))).Result;
}
