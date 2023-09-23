using Kdega.ScormEngine.Application.Common.Models;

namespace Kdega.ScormEngine.Application.Handlers.ScormLms.Commands.SetValues;

public class SetValueCommand
{
    public SetValueCommand(LmsRequest request)
    {
        Request = request;
    }

    public LmsRequest Request { get; set; }
}