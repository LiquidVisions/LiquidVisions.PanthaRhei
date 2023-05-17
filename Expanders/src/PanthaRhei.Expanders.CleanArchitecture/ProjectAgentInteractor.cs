using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal class ProjectAgentInteractor : IProjectAgentInteractor
    {
        private readonly App app;
        private readonly GenerationOptions options;

        public ProjectAgentInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            options = dependencyFactory.Get<GenerationOptions>();
        }

        public string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(options.OutputFolder, app.FullName, "src", $"{component.Name}");
        }

        public string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{component.Name}.csproj");
        }
    }
}
