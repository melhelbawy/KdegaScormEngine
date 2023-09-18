using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kdega.ScormEngine.Persistence.EntitiesConfigurations;
public class CmiCoreConfiguration : IEntityTypeConfiguration<CmiCore>
{
    public void Configure(EntityTypeBuilder<CmiCore> builder)
    {
        builder.HasOne(x => x.LearnerScormPackage)
            .WithOne(x => x.CmiCore);

        builder.HasMany(x => x.CmiData)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);
        builder.HasMany(x => x.CmiObjectives)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);
        builder.HasMany(x => x.CmiLearnerPreferences)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);
        builder.HasMany(x => x.CmiInteractions)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);
        builder.HasMany(x => x.CommentFromLearners)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);
        builder.HasMany(x => x.CommentFromLms)
            .WithOne(x => x.CmiCore)
            .HasForeignKey(x => x.CmiCoreId);

    }
}

public class CmiInteractionConfiguration : IEntityTypeConfiguration<CmiInteraction>
{
    public void Configure(EntityTypeBuilder<CmiInteraction> builder)
    {

        builder.HasMany(x => x.InteractionsCorrectResponses)
            .WithOne(x => x.CmiInteraction)
            .HasForeignKey(x => x.InteractionId);
        builder.HasMany(x => x.InteractionsObjectives)
            .WithOne(x => x.CmiInteraction)
            .HasForeignKey(x => x.InteractionId);

    }
}
