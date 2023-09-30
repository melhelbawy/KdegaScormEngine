using Kdega.ScormEngine.Application.Common.Models;

namespace Kdega.ScormEngine.Application.Interfaces;
public interface IScormApiHandler
{
    Task<LmsRequest> SetValue(string scormApiKey, LmsRequest request);
    Task<LmsRequest> GetValue(string scormApiKey, LmsRequest request);
}
