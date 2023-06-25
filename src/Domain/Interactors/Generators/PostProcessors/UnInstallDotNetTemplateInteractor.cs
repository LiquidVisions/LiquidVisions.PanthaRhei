﻿using System.IO;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.PostProcessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    internal sealed class UnInstallDotNetTemplateInteractor<TExpander> : PostProcessorInteractor<TExpander>
        where TExpander : class, IExpander
    {
        private readonly ICommandLineInteractor commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnInstallDotNetTemplateInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public UnInstallDotNetTemplateInteractor(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
            directoryService = dependencyFactory.Get<IDirectory>();
            logger = dependencyFactory.Get<ILogger>();
        }

        /// <inheritdoc/>
        public override bool Enabled => Options.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpander"/>.
        /// </summary>
        public override void Execute()
        {
            string templatePath = Path.Combine(Options.ExpandersFolder, Expander.Model.Name, Expander.Model.TemplateFolder);

            string[] dotnetTemplateDirectories = directoryService.GetDirectories(templatePath, ".template.config", SearchOption.AllDirectories);
            foreach (string dotnetTemplateDirectory in dotnetTemplateDirectories)
            {
                string path = directoryService.GetNameOfParentDirectory(dotnetTemplateDirectory);

                logger.Info($"Uninstalling template from location {path}");
                commandLine.Start($"dotnet new uninstall {path}");
            }
        }
    }
}
