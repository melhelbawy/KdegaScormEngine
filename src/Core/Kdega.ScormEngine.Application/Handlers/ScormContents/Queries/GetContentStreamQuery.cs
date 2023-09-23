using Kdega.ScormEngine.Application.Interfaces;
using MediatR;

namespace Kdega.ScormEngine.Application.Handlers.ScormContents.Queries;
public class GetContentStreamQuery : IRequest<Stream>
{
    public string Path { get; set; } = null!;
}

public class GetContentStreamQueryHandler : IRequestHandler<GetContentStreamQuery, Stream>
{
    private readonly IObjectManager _objectManager;

    public GetContentStreamQueryHandler(IObjectManager objectManager)
    {
        _objectManager = objectManager;
    }

    public async Task<Stream> Handle(GetContentStreamQuery request, CancellationToken cancellationToken)
    {
        return await _objectManager.GetObjectStreamAsync(request.Path);
    }
}