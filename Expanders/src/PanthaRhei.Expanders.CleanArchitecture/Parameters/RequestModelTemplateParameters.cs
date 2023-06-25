using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Parameters
{
    internal class RequestModelTemplateParameters : IElementTemplateParameters
    {
        public string ElementType => Resources.RequestModelElementType;

        public string NamePostfix => Resources.RequestModelNamePostfix;
    }
}
