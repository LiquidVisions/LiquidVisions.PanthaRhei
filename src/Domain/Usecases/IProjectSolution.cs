using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    public interface IProjectSolution
    {
        void CreateLibrary(string templateName, Component component);

        void CreateLibrary(string templateName, string componentName);

        void ApplyComponentPackages(Component component);

        void InitProjectSolution();
    }
}
