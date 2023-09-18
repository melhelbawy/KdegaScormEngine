using Kdega.ScormEngine.Domain.Constants.ScormLms;
using Kdega.ScormEngine.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Domain.Entities.ScormPackages;
public class Resource : EntityCreation
{
    public string Identifier { get; set; } = null!;
    public string Type { get; set; } = LmsClient.ResourceType;
    public string? Href { get; set; }
    public string? XmlBase { get; set; }
    public string? AdlcpScormType { get; set; } = LmsClient.DefaultAdlcpScormType;
    public string? Metadata { get; set; }

    public Guid ScormPackageId { get; set; }
    public ScormPackage ScormPackage { get; set; } = null!;
    public virtual ICollection<ResourceDependency>? Dependencies { get; set; }
    public virtual ICollection<ResourceFile>? Files { get; set; }
}
