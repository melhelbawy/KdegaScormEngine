using System.Xml;

namespace Kdega.ScormEngine.Application.ScormXmlObject;
public class ScormDocumentParser
{
    public static XmlNode? ParseXmlNode(XmlDocument xmlDocument, string elementName)
    {
        var nodeList = xmlDocument.GetElementsByTagName(elementName);
        return nodeList.Count > 0 ? nodeList[0] : null;
    }
}
