namespace Kdega.ScormEngine.Domain.Entities.Base;

public abstract class EntityCreation : IEntity
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; } = false;
}
