using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    public interface IProjectAgentInteractor
    {
        string GetComponentOutputFolder(Component component);

        string GetComponentProjectFile(Component component);
    }
}
