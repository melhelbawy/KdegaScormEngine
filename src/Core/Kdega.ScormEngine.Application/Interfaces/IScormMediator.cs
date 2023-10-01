namespace Kdega.ScormEngine.Application.Interfaces;
public interface IScormMediator
{
    Task Register();
    Task Handle<T>(string keyName, T request);
}
