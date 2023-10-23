using Kdega.ScormEngine.Application.Handlers.LmsSessions.Commands;
using Kdega.ScormEngine.Application.Handlers.ScormLearners.Models;
using Kdega.ScormEngine.Application.Handlers.ScormLearners.Queries;
using Kdega.ScormEngine.Domain.Constants.ScormLms;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kdega.ScormEngine.API.Pages;

public class KdegaScormPlayerModel : PageModel
{
    private readonly IMediator _mediator;

    public string? SessionId { get; set; }
    public string? LearnerId { get; set; }
    public string? LaunchParameters { get; set; }
    public string? IFrameSrc { get; set; }

    private PackageLaunchParams _learnerLaunchParams = new();

    public KdegaScormPlayerModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void OnGet()
    {

        _learnerLaunchParams = GetPackageLaunchParams(Request.Query["learnerPackageId"]!).Result;
        LearnerId = _learnerLaunchParams.LearnerId;
        SessionId = GetScoSessionId(_learnerLaunchParams.CmiCoreId.ToString(), LearnerId);
        LaunchParameters = SetupJavaScript();
        IFrameSrc = $"api/v1/scormpackages/files/{_learnerLaunchParams.FolderPath}/{_learnerLaunchParams.IndexPath}";

    }
    private string SetupJavaScript()
    {
        return $@"var lmsClient = lmsClient || {{}};
                          lmsClient.sessionId = '{SessionId}';
                          lmsClient.learnerId = '{_learnerLaunchParams.LearnerId}';
                          lmsClient.coreId = '{_learnerLaunchParams.CmiCoreId}';
                          lmsClient.learnerPackageId = '{_learnerLaunchParams.Id}';
                          lmsClient.sco_identifier = '{_learnerLaunchParams.DefaultOrganizationIdentifier}'; 
                          lmsClient.scorm_package_id = '{_learnerLaunchParams.Id}';
                          lmsClient.scoAddress = '{_learnerLaunchParams.FolderPath}';
                          lmsClient.scoFrameClientID = '{LmsClient.ScoFrameClientId}';
                          lmsClient.divDebugID = '{LmsClient.DivDebug}';
                          lmsClient.bDebug = {LmsClient.BDebug};
                          lmsClient.DateCreated = '{DateTime.Today}'; ";

    }

    private string GetScoSessionId(string coreId, string learnerId) =>
        _mediator.Send(new InitLearnerSessionCommand()
        {
            CmiCoreId = coreId,
            ScoIdentifier = "default",
            LearnerId = learnerId,
        }).Result;

    private async Task<PackageLaunchParams> GetPackageLaunchParams(string learnerPackageId)
    {
        return await _mediator.Send(new GetLearnerPackageLaunchQuery(Guid.Parse(learnerPackageId)));
    }
}
