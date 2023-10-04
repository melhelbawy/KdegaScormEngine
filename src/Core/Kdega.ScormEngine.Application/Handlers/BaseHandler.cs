using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Application.Services;
using MapsterMapper;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers;
public class BaseHandler
{
    protected readonly IServiceProvider Provider;
    protected readonly IMapper Mapper;
    protected readonly IKseDbContext Context;
    private CurrentUserService? _currentUser;
    private IMediator? _mediator;

    public BaseHandler(IServiceProvider provider)
    {
        Provider = provider;
        Mapper = (IMapper)provider.GetService(typeof(IMapper))!;
        Context = provider.GetCustomRequiredService<IKseDbContext>();
    }

    protected CurrentUserService CurrentUser => _currentUser ??= Provider.GetCustomRequiredService<CurrentUserService>();
    protected IMediator Mediator => _mediator ??= Provider.GetCustomRequiredService<IMediator>();
}
