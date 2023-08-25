using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    internal class DotNetApplication : IApplication
    {
        private readonly GenerationOptions options;
        private readonly ILogger logger;
        private readonly IDirectory directory;
        private readonly IFile file;
        private readonly ICommandLine cli;
        private readonly App app;

        public DotNetApplication(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            logger = dependencyFactory.Get<ILogger>();
            directory = dependencyFactory.Get<IDirectory>();
            file = dependencyFactory.Get<IFile>();
            cli = dependencyFactory.Get<ICommandLine>();
            app = dependencyFactory.Get<App>();
        }

        public void MaterializeProject()
        {
            var root = Path.Combine(options.OutputFolder, app.FullName);

            CreateFolderIfNeeded(root);

            cli.Start($"dotnet new liquidvisions-expanders-{app.Name} --NAME {app.Name} --NS {app.FullName}", root);
        }

        public void MaterializeComponent(Component component)
        {
            var solutionRoot = Path.Combine(options.OutputFolder, app.FullName);
            var componentRoot = GetComponentRoot(component);

            CreateFolderIfNeeded(solutionRoot);
            CreateFolderIfNeeded(componentRoot);

            if(!file.Exists(Path.Combine(solutionRoot, $"{app.FullName}.sln")))
            {
                cli.Start($"dotnet new sln", solutionRoot);
            }

            cli.Start($"dotnet new liquidvisions-expanders-{component.Name} --NAME {component.Name} --NS {app.FullName}", componentRoot);
            cli.Start($"dotnet sln {Path.Combine(solutionRoot, $"{app.FullName}.sln")} add {GetComponentConfigurationFile(component)}");
        }

        public virtual string GetComponentRoot(Component component)
        {
            return Path.Combine(options.OutputFolder, app.FullName, "src", $"{component.Name}");
        }

        public virtual string GetComponentConfigurationFile(Component component)
        {
            return Path.Combine(GetComponentRoot(component), $"{component.Name}.csproj");
        }

        public void AddPackages(Component component)
        {
            throw new System.NotImplementedException();
        }

        private void CreateFolderIfNeeded(string root)
        {
            if (!directory.Exists(root))
            {
                logger.Info($"Creating directory {root}");
                cli.Start($"mkdir {root}");
            }
        }
    }
}
