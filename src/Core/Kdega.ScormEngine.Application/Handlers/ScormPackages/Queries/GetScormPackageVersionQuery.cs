using Kdega.ScormEngine.Application.Behavior;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Queries;
public class GetScormPackageVersionQuery : IRequest<string>
{
    public Guid ScormPackageId { get; set; }

    public GetScormPackageVersionQuery(Guid scormContentId)
    {
        ScormPackageId = scormContentId;
    }
}

public class GetScormPackageVersionQueryHandler : BaseHandler, IRequestHandler<GetScormPackageVersionQuery, string>
{
    public GetScormPackageVersionQueryHandler(IServiceProvider provider) : base(provider)
    {
    }
    public async Task<string> Handle(GetScormPackageVersionQuery request, CancellationToken cancellationToken)
    {
        var scormPackage = await Context.ScormPackages.FirstOrDefaultAsync(x => x.Id == request.ScormPackageId, cancellationToken);
        Check.NotNull(scormPackage, nameof(ScormPackage));

        return scormPackage.ScormVersion ?? "1.2";
    }
}