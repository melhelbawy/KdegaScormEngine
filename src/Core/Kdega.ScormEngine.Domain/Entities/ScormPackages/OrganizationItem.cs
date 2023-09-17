using Kdega.ScormEngine.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;
public class OrganizationItem : EntityCreation
{
    public string Title { get; set; } = null!;
    public string Identifier { get; set; } = null!;
    public string? IdentifierRef { get; set; }
    public bool? IsVisible { get; set; } = true;
    public string? Parameters { get; set; }
    public string? AdlcpTimeLimitAction { get; set; }
    public string? AdlcpDataFromLms { get; set; }
    public string? AdlcpCompletionThresholds { get; set; }
    public string? ImsssSequencing { get; set; }
    public string? AdlnavPresentation { get; set; }

    public Guid? ParentItemId { get; set; }
    public OrganizationItem? ParentItem { get; set; }

    public Guid? OrganizationId { get; set; }
    public Organization? Organization { get; set; }

    public virtual ICollection<OrganizationItem>? SubOrganizationItems { get; set; } = new HashSet<OrganizationItem>();

}
