using Kdega.ScormEngine.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;
public class ResourceFile : EntityCreation
{
    public string Href { get; set; } = null!;
    public string? Metadata { get; set; }

    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; } = null!;
}
