namespace Kdega.ScormEngine.Domain.Entities.Base;

public abstract class AuditableEntity : EntityCreation
{
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
