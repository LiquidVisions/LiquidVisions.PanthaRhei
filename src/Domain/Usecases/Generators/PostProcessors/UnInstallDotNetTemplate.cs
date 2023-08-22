using System.IO;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    internal sealed class UnInstallDotNetTemplate<TExpander> : PostProcessor<TExpander>
        where TExpander : class, IExpander
    {
        private readonly ICommandLine commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnInstallDotNetTemplate{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public UnInstallDotNetTemplate(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            commandLine = dependencyFactory.Get<ICommandLine>();
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
            string templatePath = Path.Combine(Options.ExpandersFolder, Expander.Model.Name, Resources.TemplatesFolder);

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
