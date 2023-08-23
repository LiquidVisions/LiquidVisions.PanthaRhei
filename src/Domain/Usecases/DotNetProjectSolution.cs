﻿using System.IO;
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

            if (!directory.Exists(outputFolder))
            {
                logger.Info($"Creating directory {outputFolder}");
                cli.Start($"mkdir {outputFolder}");
            }
        }

        public void CreateLibrary(string templateName, Component component = null)
        {
            logger.Info($"Creating {app.Name} @ {outputFolder}");
            cli.Start($"dotnet new {templateName} --NAME {app.Name} --NS {app.FullName}", Path.Combine(outputFolder, "src"));

            if (component != null)
            {
                cli.Start($"dotnet sln {Path.Combine(outputFolder, $"{app.FullName}.sln")} add {Path.Combine(componentRootPath, $"{component.Name}.csproj")}");
            }
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
