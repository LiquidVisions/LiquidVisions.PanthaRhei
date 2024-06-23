using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    internal class DotNetApplication(IDependencyFactory dependencyFactory) : IApplication
    {
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ILogger logger = dependencyFactory.Resolve<ILogger>();
        private readonly IDirectory directory = dependencyFactory.Resolve<IDirectory>();
        private readonly IFile file = dependencyFactory.Resolve<IFile>();
        private readonly ICommandLine cli = dependencyFactory.Resolve<ICommandLine>();
        private readonly App app = dependencyFactory.Resolve<App>();

        public void MaterializeProject()
        {
            string root = Path.Combine(options.OutputFolder, app.FullName);

            CreateFolderIfNeeded(root);

            cli.Start($"dotnet new liquidvisions-expanders-{app.Name} --NAME {app.Name} --NS {app.FullName}", root);
        }

        public void MaterializeComponent(Component component)
        {
            string solutionRoot = Path.Combine(options.OutputFolder, app.FullName);
            string componentRoot = GetComponentRoot(component);

            CreateFolderIfNeeded(solutionRoot);
            CreateFolderIfNeeded(componentRoot);

            if (!file.Exists(Path.Combine(solutionRoot, $"{app.FullName}.sln")))
            {
                cli.Start($"dotnet new sln", solutionRoot);
            }

            cli.Start($"dotnet new liquidvisions-expanders-{component.Name} --NAME {component.Name} --NS {app.FullName}", componentRoot);
            cli.Start($"dotnet sln {Path.Combine(solutionRoot, $"{app.FullName}.sln")} add {GetComponentConfigurationFile(component)}");
        }

        public virtual string GetComponentRoot(Component component)
        {
            string root = Path.Combine(
                options.OutputFolder,
                app.FullName,
                "src",
                $"{component.Name}");

            return root;
        }

        public virtual string GetComponentConfigurationFile(Component component)
        {
            string root = GetComponentRoot(component);
            string file = $"{component.Name}.csproj";

            string path =  Path.Combine(root, file);

            return path;
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

        public void AddReference(Component component, Component reference)
            => cli.Start($"dotnet add {GetComponentConfigurationFile(component)} reference {GetComponentConfigurationFile(reference)}");
    }
}
