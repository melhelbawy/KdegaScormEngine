using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
public class GetScormPackageHrefQuery : IRequest<string>
{
    public Guid ScormPackageId { get; set; }

    public GetScormPackageHrefQuery(Guid scormContentId)
    {
        ScormPackageId = scormContentId;
    }
}

public class GetContentHrefQueryHandler : BaseHandler, IRequestHandler<GetScormPackageHrefQuery, string>
{
    public GetContentHrefQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<string> Handle(GetScormPackageHrefQuery request, CancellationToken cancellationToken)
    {
        var scormContent = await Context.ScormPackages.FirstOrDefaultAsync(x => x.Id == request.ScormPackageId, cancellationToken);
        return scormContent?.IndexPath ?? "";
    }
}