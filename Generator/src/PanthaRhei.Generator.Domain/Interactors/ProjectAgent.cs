using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors
{
    public interface IProjectAgentInteractor
    {
        string GetComponentOutputFolder(Component component);

        string GetComponentProjectFile(Component component);
    }

    internal class ProjectAgent : IProjectAgentInteractor
    {
        private readonly App app;
        private readonly Parameters parameters;

        public ProjectAgent(IDependencyFactoryInteractor dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            parameters = dependencyFactory.Get<Parameters>();
        }

        public string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(parameters.OutputFolder, app.FullName, "src", $"{app.Name}.{component.Name}");
        }

        public string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{app.Name}.{component.Name}.csproj");
        }
    }
}
