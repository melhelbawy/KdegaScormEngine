namespace Kdega.ScormEngine.Domain.Entities.Base;
public class ItemEntity : AuditableEntity
{
    public string Title { get; set; } = null!;
    public string? TitleEn { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
}
