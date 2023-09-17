using Kdega.ScormEngine.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;
public class Organization : AuditableEntity
{
    public string Title { get; set; } = null!;
    public string Identifier { get; set; } = null!;
    public string? Structure { get; set; }
    public string? AdlseqObjectivesGlobalToSystem { get; set; } = "false";
    public string? ImsssSequencing { get; set; }
    public string? MetadataLocation { get; set; }

    public Guid ScormPackageId { get; set; }
    public ScormPackage ScormPackage { get; set; } = null!;

    public virtual ICollection<OrganizationItem> Items { get; set; } = new HashSet<OrganizationItem>();
}
