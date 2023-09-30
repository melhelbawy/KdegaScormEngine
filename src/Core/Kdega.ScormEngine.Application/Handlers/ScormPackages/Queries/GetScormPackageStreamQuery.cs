using Kdega.ScormEngine.Application.Interfaces;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
public class GetScormPackageStreamQuery : IRequest<Stream>
{
    public string Path { get; set; } = null!;
}

public class GetContentStreamQueryHandler : IRequestHandler<GetScormPackageStreamQuery, Stream>
{
    private readonly IObjectManager _objectManager;

    public GetContentStreamQueryHandler(IObjectManager objectManager)
    {
        _objectManager = objectManager;
    }

    public async Task<Stream> Handle(GetScormPackageStreamQuery request, CancellationToken cancellationToken)
    {
        return await _objectManager.GetObjectStreamAsync(request.Path);
    }
}