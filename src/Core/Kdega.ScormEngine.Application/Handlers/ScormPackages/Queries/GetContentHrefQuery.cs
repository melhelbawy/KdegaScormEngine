using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
public class GetContentHrefQuery : IRequest<string>
{
    public Guid ScormPackageId { get; set; }

    public GetContentHrefQuery(Guid scormContentId)
    {
        ScormPackageId = scormContentId;
    }
}

public class GetContentHrefQueryHandler : BaseHandler<ScormPackage>, IRequestHandler<GetContentHrefQuery, string>
{
    public GetContentHrefQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<string> Handle(GetContentHrefQuery request, CancellationToken cancellationToken)
    {
        var scormContent = await Context.ScormPackages.FirstOrDefaultAsync(x => x.Id == request.ScormPackageId, cancellationToken);
        return scormContent?.IndexPath ?? "";
    }
}