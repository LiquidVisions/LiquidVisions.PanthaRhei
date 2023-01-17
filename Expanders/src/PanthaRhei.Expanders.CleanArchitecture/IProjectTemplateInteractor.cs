using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal interface IProjectTemplateInteractor
    {
        void ApplyPackageOnComponent(Component component, Package package);

        void CreateNew(string commandParameters);
    }
}