using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    internal class DotNetProjectSolution : IProjectSolution
    {
        private readonly GenerationOptions options;
        private readonly ILogger logger;
        private readonly IDirectory directory;
        private readonly ICommandLine cli;
        private readonly App app;

        private readonly string outputFolder;
        private readonly string componentRootPath;

        public DotNetProjectSolution(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            logger = dependencyFactory.Get<ILogger>();
            directory = dependencyFactory.Get<IDirectory>();
            cli = dependencyFactory.Get<ICommandLine>();
            app = dependencyFactory.Get<App>();

            outputFolder = Path.Combine(options.OutputFolder, app.FullName);
            componentRootPath = Path.Combine(outputFolder, "src");

            CreateIfNotExis(outputFolder);
            CreateIfNotExis(componentRootPath);
        }

        private void CreateIfNotExis(string folder)
        {
            if (!directory.Exists(folder))
            {
                logger.Info($"Creating directory {folder}");
                cli.Start($"mkdir {folder}");
            }
        }

        public void CreateLibrary(string templateName, Component component) 
            => CreateLibrary(templateName, component.Name);

        public void CreateLibrary(string templateName, string componentName)
        {
            string componentFolder = Path.Combine(componentRootPath, componentName);
            CreateIfNotExis(componentFolder);

            cli.Start($"dotnet new {templateName} --NAME {componentName} --NS {app.FullName}", componentFolder);
            cli.Start($"dotnet sln {Path.Combine(outputFolder, $"{app.FullName}.sln")} add {Path.Combine(componentFolder, $"{componentName}.csproj")}");
        }

        internal virtual string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(options.OutputFolder, app.FullName, "src", $"{component.Name}");
        }

        internal virtual string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{component.Name}.csproj");
        }

        public void ApplyComponentPackages(Component component)
        {
            throw new System.NotImplementedException();
        }

        public void InitProjectSolution()
        {
            cli.Start("dotnet new sln", outputFolder);
        }
    }
}
