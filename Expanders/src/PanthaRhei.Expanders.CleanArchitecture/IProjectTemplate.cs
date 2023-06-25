using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal interface IProjectTemplate
    {
        void ApplyPackageOnComponent(Component component, Package package);

        void CreateNew(string commandParameters);
    }
}