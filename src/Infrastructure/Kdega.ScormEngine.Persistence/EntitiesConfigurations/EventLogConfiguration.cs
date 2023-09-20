using Kdega.ScormEngine.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kdega.ScormEngine.Persistence.EntitiesConfigurations;
public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder
            .Property(x => x.Id)
            .HasDefaultValueSql("newid()");
    }
}
