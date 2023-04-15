using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    internal class ProjectAgentInteractor : IProjectAgentInteractor
    {
        private readonly App app;
        private readonly ExpandRequestModel expandRequestModel;

        public ProjectAgentInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            expandRequestModel = dependencyFactory.Get<ExpandRequestModel>();
        }

        public string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(expandRequestModel.OutputFolder, app.FullName, "src", $"{component.Name}");
        }

        public string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{component.Name}.csproj");
        }
    }
}
