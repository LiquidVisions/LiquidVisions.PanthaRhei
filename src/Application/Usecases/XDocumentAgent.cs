using System.Xml.Linq;

namespace LiquidVisions.PanthaRhei.Application.Usecases
{
    internal interface IXDocument
    {
        XDocument Load(string path);
    }

    internal class XDocumentAgent : IXDocument
    {
        public XDocument Load(string path) => XDocument.Load(path);
    }
}
