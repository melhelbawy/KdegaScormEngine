using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kdega.ScormEngine.Persistence.EntitiesConfigurations;
public class ScormPackageConfiguration : IEntityTypeConfiguration<ScormPackage>
{
    public void Configure(EntityTypeBuilder<ScormPackage> builder)
    {
        builder.HasMany(x => x.Resources)
            .WithOne(x => x.ScormPackage)
            .HasForeignKey(x => x.ScormPackageId);
        builder.HasMany(x => x.Organizations)
            .WithOne(x => x.ScormPackage)
            .HasForeignKey(x => x.ScormPackageId);
        builder.HasMany(x => x.LearnersScorms)
            .WithOne(x => x.ScormPackage)
            .HasForeignKey(x => x.ScormPackageId);
    }
}
public class ScormPackageResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasMany(x => x.Dependencies)
            .WithOne(x => x.Resource)
            .HasForeignKey(x => x.ResourceId);
        builder.HasMany(x => x.Files)
            .WithOne(x => x.Resource)
            .HasForeignKey(x => x.ResourceId);
    }
}

public class ScormPackageOrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);
    }
}

public class ScormPackageOrganizationItemConfiguration : IEntityTypeConfiguration<OrganizationItem>
{
    public void Configure(EntityTypeBuilder<OrganizationItem> builder)
    {
        builder.HasMany(x => x.SubOrganizationItems)
            .WithOne(x => x.ParentItem)
            .HasForeignKey(x => x.ParentItemId);
    }
}