﻿using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    /// <summary>
    /// Concrete implementation of <seealso cref="IProcessor"/>.
    /// </summary>
    internal class DotNetTemplateInteractor : IProjectTemplateInteractor
    {
        private readonly ICommandLineInteractor commandLine;
        private readonly ILogger logger;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetTemplateInteractor"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public DotNetTemplateInteractor(IDependencyFactory dependencyFactory)
        {
            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
            logger = dependencyFactory.Get<ILogger>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            expander = dependencyFactory.Get<CleanArchitectureExpander>();
        }

        /// <summary>
        /// Creates a .csproj with the dotnet new command and and adds it to the solution.
        /// </summary>
        /// <param name="commandParameters">The dotnet cli command options.</param>
        public virtual void CreateNew(string commandParameters)
        {
            string name = app.Name;
            string ns = app.FullName;

            string outputFolder = Path.Combine(options.OutputFolder, ns);

            logger.Info($"Creating directory {outputFolder}");
            commandLine.Start($"mkdir {outputFolder}");

            logger.Info($"Creating {name} @ {outputFolder}");
            commandLine.Start($"dotnet new {commandParameters} --NAME {name} --ns {ns}", outputFolder);
        }

        public void ApplyPackageOnComponent(Component component, Package package)
        {
            string fullPathToProject = expander.GetComponentProjectFile(component);

            logger.Info($"Adding nuget package {package.Name} to {fullPathToProject}");

            commandLine.Start($"dotnet add \"{fullPathToProject}\" package \"{package.Name}\" --version {package.Version} -n");
        }
    }
}
