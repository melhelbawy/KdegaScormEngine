using Kdega.ScormEngine.Application.Attributes;
using System.Reflection;

namespace Kdega.ScormEngine.Application.Behavior.Logging;
public static class LoggingHelper
{
    public static object? TreatSensitivity(this PropertyInfo property, object request)
    {
        return Attribute.IsDefined(property, typeof(SensitiveAttribute)) ? "***CAPTURED***" : property.GetValue(request, null);
    }
}
