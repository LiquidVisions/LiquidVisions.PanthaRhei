using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    public sealed class UnInstallDotNetTemplate<TExpander> : PostProcessor<TExpander>
        where TExpander : class, IExpander
    {
        private readonly ICommandLine commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnInstallDotNetTemplate{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public UnInstallDotNetTemplate(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            commandLine = dependencyResolver.Get<ICommandLine>();
            directoryService = dependencyResolver.Get<IDirectory>();
            logger = dependencyResolver.Get<ILogger>();
        }

        /// <inheritdoc/>
        public override bool CanExecute => Parameters.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpander"/>.
        /// </summary>
        public override void Execute()
        {
            string templatePath = Path.Combine(Parameters.ExpandersFolder, Expander.Model.Name, Expander.Model.TemplateFolder);

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
