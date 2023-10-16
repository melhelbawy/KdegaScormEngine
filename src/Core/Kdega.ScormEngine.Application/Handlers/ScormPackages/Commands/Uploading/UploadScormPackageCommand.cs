using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Commands.Uploading;
public class UploadScormPackageCommand : IRequest<bool>
{
    public string PackageTitle { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public IFormFile ScormPackage { get; set; } = null!;
}

public class UploadScormPackageCommandHandler : BaseHandler, IRequestHandler<UploadScormPackageCommand, bool>
{
    private readonly IObjectManager _objectManager;
    private readonly IMediator _mediator;
    private readonly IDirectoryManger _directoryManger;


    public UploadScormPackageCommandHandler(IServiceProvider provider,
        IObjectManager objectManager, IMediator mediator,
        IDirectoryManger directoryManger) : base(provider)
    {
        _objectManager = objectManager;
        _mediator = mediator;
        _directoryManger = directoryManger;
    }

    public async Task<bool> Handle(UploadScormPackageCommand request, CancellationToken cancellationToken)
    {
        if (request.ScormPackage.Length <= 0) return false;
        var scormPath = await _objectManager.UnZipFileTo(request.ScormPackage);
        var scormFiles = await _directoryManger.ListDirectoryAsync(scormPath);


        var entry = new ScormPackage()
        {
            PackageFolderPath = scormPath,
            UploadingDate = DateTime.Now,
            TitleFromUpload = request.PackageTitle,
        };

        await Context.ScormPackages.AddAsync(entry, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var scormXml = await _mediator.Send(new ParseManifestCommand()
        {
            ScormPackageId = entry.Id,
            PathToManifest = _objectManager.FileFullPath(scormFiles?.FirstOrDefault(c => c.Name.Contains("manifest"))?.Path!)

        }, cancellationToken);


        return true;
    }
}