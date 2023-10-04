using Kdega.ScormEngine.Application.Handlers.ScormPackages.Models;
using Kdega.ScormEngine.Application.ScormXmlObject;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Mapster;
using MediatR;
using System.Xml;

namespace Kdega.ScormEngine.Application.Handlers.ScormPackages.Commands.Uploading;
public class ParseManifestCommand : IRequest<ScormPackageManifestDto>
{
    public Guid ScormPackageId { get; set; }
    public string PathToManifest { get; set; } = null!;
}
public class ParseManifestCommandHandler : BaseHandler, IRequestHandler<ParseManifestCommand, ScormPackageManifestDto>
{
    public ParseManifestCommandHandler(IServiceProvider provider) : base(provider)
    {
    }

    public async Task<ScormPackageManifestDto> Handle(ParseManifestCommand request, CancellationToken cancellationToken)
    {
        var scormPackage = Context.ScormPackages.FirstOrDefault(x => x.Id == request.ScormPackageId);

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(request.PathToManifest);
        var manifest = ScormDocumentParser.ParseXmlNode(xmlDocument, "manifest");

        scormPackage!.ManifestIdentifier = manifest?.Attributes?["identifier"]?.InnerText ?? "";
        scormPackage.ManifestVersion = manifest?.Attributes?["version"]?.InnerText;
        scormPackage.ManifestXmlBase = manifest?.Attributes?["xml:base"]?.InnerText;

        var metadata = ScormDocumentParser.ParseXmlNode(xmlDocument, "metadata");

        var schemaVersion = GetChildElementInnerText(metadata!, "schemaversion")!;
        scormPackage.ManifestMetadataSchema = GetChildElementInnerText(metadata!, "schema")!;
        scormPackage.ManifestMetadataSchemaVersion = GetChildElementInnerText(metadata!, "schemaversion")!;
        scormPackage.ManifestMetadataLocation = GetChildElementInnerText(metadata!, "adlcp:location")!;

        var organizations = ScormDocumentParser.ParseXmlNode(xmlDocument, "organizations");
        scormPackage.DefaultOrganizationIdentifier = organizations?.Attributes?["default"]?.InnerText;
        var packageOrganizations = MapOrganizations(organizations!, scormPackage);
        scormPackage.Organizations = packageOrganizations;

        var resources = ScormDocumentParser.ParseXmlNode(xmlDocument, "resources");

        scormPackage.ResourceXmlBase = resources?.Attributes?["xml:base"]?.InnerText;
        var packageResources = MapManifestResources(resources!, scormPackage);

        scormPackage.TitleFromManifest = packageOrganizations.FirstOrDefault()?.Title;
        scormPackage.IndexPath = packageResources.FirstOrDefault()?.Href!;
        scormPackage.ScormVersion = schemaVersion.Equals("1.2") ? "1.2" : "1.3";

        Context.ScormPackages.Update(scormPackage);

        await Context.SaveChangesAsync(cancellationToken);

        return scormPackage.Adapt<ScormPackageManifestDto>();
    }

    static string? GetChildElementInnerText(XmlNode parent, string childElementName)
    {
        foreach (XmlNode childNode in parent.ChildNodes)
        {
            if (childNode is XmlElement element && element.Name == childElementName)
                return childNode.InnerText;
        }
        return null;
    }

    private List<Organization> MapOrganizations(XmlNode organizations, ScormPackage package)
    {
        foreach (XmlNode organization in organizations.ChildNodes)
        {
            if (organization is not XmlElement { Name: "organization" }) continue;

            var packageOrganization = new Organization()
            {
                Identifier = organization.Attributes?["identifier"]?.InnerText ?? "",
                Structure = organization.Attributes?["adlseq:objectivesGlobalToSystem"]?.InnerText ?? null,
                AdlseqObjectivesGlobalToSystem = organization.Attributes?["structure"]?.InnerText ?? "false",
                Title = GetChildElementInnerText(organization, "title")!,
                MetadataLocation = GetChildElementInnerText(organization, "metadata")!,
                ImsssSequencing = GetChildElementInnerText(organization, "imsss:sequencing")!,
            };
            packageOrganization.Items = MapOrganizationItems(organization, packageOrganization);

            package.Organizations.Add(packageOrganization);
        }
        return package.Organizations.ToList();
    }

    private static List<OrganizationItem> MapOrganizationItem(XmlNode itemRoot, OrganizationItem parentItem)
    {
        foreach (XmlNode item in itemRoot.ChildNodes)
        {
            if (item.Name is "#comment" or "#text" or "title") continue;

            parentItem.SubOrganizationItems?.Add(new OrganizationItem()
            {
                Identifier = item.Attributes?["identifier"]?.InnerText ?? "",
                IdentifierRef = item.Attributes?["identifierref"]?.InnerText ?? "",
                IsVisible = item.Attributes?["identifier"]?.InnerText == "true" ? true : false,
                Parameters = item.Attributes?["parameters"]?.InnerText ?? "",
                Title = GetChildElementInnerText(item, "title")!,
                AdlcpTimeLimitAction = GetChildElementInnerText(item, "adlcp:timeLimitAction")!,
                AdlcpDataFromLms = GetChildElementInnerText(item, "adlcp:dataFromLMS")!,
                AdlcpCompletionThresholds = GetChildElementInnerText(item, "adlcp:completionThreshold")!,
                ImsssSequencing = GetChildElementInnerText(item, "imsss:sequencing")!,
                AdlnavPresentation = GetChildElementInnerText(item, "adlnav:presentation")!,
                SubOrganizationItems = MapOrganizationItem(item, parentItem)
            });
        }
        return parentItem.SubOrganizationItems?.ToList()!;
    }

    private List<OrganizationItem> MapOrganizationItems(XmlNode organization, Organization pkgOrganization)
    {
        var items = new List<OrganizationItem>();
        foreach (XmlNode item in organization.ChildNodes)
        {
            if (item.Name == "#comment") continue;

            if (item.NodeType != XmlNodeType.Element || !item.HasChildNodes ||
                item.LastChild?.Name == "#text") continue;
            var pkgItem = new OrganizationItem
            {
                Identifier = item.Attributes?["identifier"]?.InnerText ?? "",
                IdentifierRef = item.Attributes?["identifierref"]?.InnerText ?? "",
                IsVisible = item.Attributes?["identifier"]?.Value == "true" ? true : false,
                Parameters = item.Attributes?["parameters"]?.InnerText ?? "",
                Title = GetChildElementInnerText(item, "title")!,
                AdlcpTimeLimitAction = GetChildElementInnerText(item, "adlcp:timeLimitAction")!,
                AdlcpDataFromLms = GetChildElementInnerText(item, "adlcp:dataFromLMS")!,
                AdlcpCompletionThresholds = GetChildElementInnerText(item, "adlcp:completionThreshold")!,
                ImsssSequencing = GetChildElementInnerText(item, "imsss:sequencing")!,
                AdlnavPresentation = GetChildElementInnerText(item, "adlnav:presentation")!,
            };

            pkgItem.SubOrganizationItems = MapOrganizationItem(item, pkgItem);
            pkgOrganization.Items.Add(pkgItem);
        }
        return items;
    }

    private IEnumerable<Resource> MapManifestResources(XmlNode resources, ScormPackage package)
    {
        var response = new List<Resource>();
        foreach (XmlNode resource in resources.ChildNodes)
        {
            if (resource.Name is "#comment" or "#text" or "title") continue;

            var packageResource = new Resource()
            {
                Identifier = resource.Attributes?["identifier"]?.InnerText ?? "",
                Type = resource.Attributes?["type"]?.InnerText ?? "webcontent",
                Href = resource.Attributes?["href"]?.InnerText,
                XmlBase = resource.Attributes?["xml:base"]?.InnerText,
                AdlcpScormType = resource.Attributes?["adlcp:scormType"]?.InnerText,
            };
            packageResource.Files = MapResourceFiles(resource, packageResource);
            package.Resources.Add(packageResource);
        }
        return response;
    }

    private List<ResourceFile> MapResourceFiles(XmlNode resource, Resource packageResource)
    {
        var response = new List<ResourceFile>();
        foreach (XmlNode file in resource.ChildNodes)
        {
            if (file.Name is "#comment" or "#text" or "title" or not "file") continue;

            packageResource.Files?.Add(new ResourceFile()
            {
                Href = resource.Attributes?["href"]?.InnerText!,
                Metadata = GetChildElementInnerText(file, "metadata")
            });
        }
        return response;
    }

    private List<ResourceDependency> MapResourceDependencies(XmlNode resource, ScormPackage package)
    {
        var response = new List<ResourceDependency>();
        foreach (XmlNode file in resource.ChildNodes)
        {
            if (file.Name is "#comment" or "#text" or "title" or not "dependency") continue;

            response.Add(new ResourceDependency()
            {
                IdentifierRef = resource.Attributes?["identifierref"]?.InnerText!,
            });
        }


        return response;
    }
}