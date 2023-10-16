using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kdega.ScormEngine.Persistence.EntitiesConfigurations;

public class LearnerScormPackageConfiguration : IEntityTypeConfiguration<LearnerScormPackage>
{
    public void Configure(EntityTypeBuilder<LearnerScormPackage> builder)
    {
        builder.HasMany(x => x.CmiCores)
            .WithOne(x => x.LearnerScormPackage)
            .HasForeignKey(x => x.LearnerScormPackageId);
    }
}