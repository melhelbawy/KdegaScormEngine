using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Mapster;

namespace Kdega.ScormEngine.Application.Handlers.ScormLearners.Models;
public class LearnerScormPackageDto : IRegister
{

    public string? LearnerId { get; set; }
    public Guid ScormPackageId { get; set; }
    public string ScormPackageTitle { get; set; } = null!;
    public DateTime? JoiningDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime? DatePassed { get; set; }
    public DateTime? LastOpenedDate { get; set; }
    public decimal? Score { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.ForType<LearnerScormPackage, LearnerScormPackageDto>()
            .Map(dest => dest.ScormPackageTitle, src => src.ScormPackage.TitleFromUpload)
            .Map(dest => dest.ScormPackageId, src => src.ScormPackageId);
    }
}
