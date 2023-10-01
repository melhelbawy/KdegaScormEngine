namespace Kdega.ScormEngine.Application.Attributes;
[AttributeUsage(AttributeTargets.Class)]
public class ScormMediatorComponentAttribute : Attribute
{
    public string ComponentName { get; }

    public ScormMediatorComponentAttribute(string componentName)
    {
        ComponentName = componentName;
    }
}
