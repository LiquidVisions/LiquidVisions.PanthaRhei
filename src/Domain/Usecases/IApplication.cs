using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    public interface IApplication
    {
        void AddPackages(Component component);

        string GetComponentConfigurationFile(Component component);

        string GetComponentRoot(Component component);

        void MaterializeComponent(Component component);

        void MaterializeProject();
    }
}
