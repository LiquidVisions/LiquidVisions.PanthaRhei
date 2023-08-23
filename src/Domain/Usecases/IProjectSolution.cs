using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    public interface IProjectSolution
    {
        void CreateComponentLibrary(Component component, string templateName);

        void ApplyComponentPackages(Component component);

        void InitProjectSolution();
    }
}
