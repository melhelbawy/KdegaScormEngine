using Kdega.ScormEngine.Application.Common.Models;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Queries.GetValues;
public class GetValueQuery
{
    public GetValueQuery(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }
}
