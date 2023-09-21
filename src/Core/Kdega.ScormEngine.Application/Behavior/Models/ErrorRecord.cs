namespace Kdega.ScormEngine.Application.Behavior.Models;
/// <summary>
/// Model validation Error record
/// </summary>
/// <param name="PropertyName"></param>
/// <param name="Type"></param>
/// <param name="Message"></param>
public record ErrorRecord(string PropertyName, string Type, string Message);