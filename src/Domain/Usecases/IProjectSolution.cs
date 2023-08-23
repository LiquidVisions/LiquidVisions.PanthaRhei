using LiquidVisions.PanthaRhei.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    public interface IProjectSolution
    {
        void CreateLibrary(string templateName, Component component = null);

        void ApplyComponentPackages(Component component);

        void InitProjectSolution();
    }
}
