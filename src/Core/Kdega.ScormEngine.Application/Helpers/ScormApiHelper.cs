using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Handlers.LearnerCmiCores.Commands;
using MediatR;

namespace Kdega.ScormEngine.Application.Helpers;

public class ScormApiHelper
{
    private static bool IsValidKey(string scormKey, LmsRequest request) =>
        ScormKeyCommands(request).ContainsKey(scormKey);

    public static List<IRequest<LmsRequest>> GetScormKeyCommands(string scormKey, LmsRequest request) =>
        IsValidKey(scormKey, request) ? ScormKeyCommands(request)[scormKey] :
            throw new Exception("Not valid Scorm api key");

    private static Dictionary<string, List<IRequest<LmsRequest>>> ScormKeyCommands(LmsRequest request) =>
        new Dictionary<string, List<IRequest<LmsRequest>>>()
        {
            {
                "cmi.core.exit", new List<IRequest<LmsRequest>>
                {
                    new UpdateCmiCoreExitCommand()
                }

            }
        };


}