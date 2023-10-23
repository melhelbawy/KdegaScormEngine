using Kdega.ScormEngine.Application.Enums;

namespace Kdega.ScormEngine.Application.Attributes;
[AttributeUsage(AttributeTargets.Class)]
public class ScormMediatorComponentAttribute : Attribute
{
    public string ComponentName { get; }
    public ScormApiMethod ApiMethod { get; }

    public ScormMediatorComponentAttribute(string componentName, ScormApiMethod apiMethod)
    {
        ComponentName = componentName;
        ApiMethod = apiMethod;
    }
}
