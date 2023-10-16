using Kdega.ScormEngine.Application.Constants;
using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Queries;
using Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormClients.Models;
using Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;
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
        var lmsIndex = Uri.EscapeDataString(Request.Query["path"]);

        LearnerId = Request.Query["learnerId"]!;
        ScormPackageId = Request.Query["packageId"]!;

        ScormLearnerPackageId = GetLearnerPackageId(ScormPackageId);

        var scoLaunch = GetCourseInformation(LearnerId);
        LaunchParameters = SetupJavaScript(scoLaunch);
        IFrameSrc = $"api/v1/scormpackages/files/{lmsIndex}";

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
                          lmsClient.divDebugID = '{LmsClient.DivDebug}';
                          lmsClient.bDebug = {LmsClient.BDebug};
                          lmsClient.DateCreated = '{DateTime.Today}'; ";

    }

    private LmsClientDto GetCourseInformation(string learnerId)
    {
        var scoLaunch = new LmsClientDto
        {
            ScormPackageId = ScormPackageId,
            LearnerId = learnerId,
        };

        scoLaunch.ScoAddress = _mediator.Send(new GetScormPackageHrefQuery(Guid.Parse(ScormPackageId!)), CancellationToken.None).Result;

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
        _mediator.Send(new GetCurrentLearnerCmiCoreIdQuery(ScormLearnerPackageId!)).Result;

    private string GetLearnerPackageId(string scormPackageId) =>
        _mediator.Send(new GetCurrentLearnerPackageIdQuery()
        {
            LearnerId = LearnerId!,
            ScormPackageId = scormPackageId
        }).Result;
}
