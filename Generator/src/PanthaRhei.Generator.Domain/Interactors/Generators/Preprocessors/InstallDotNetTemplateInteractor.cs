using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Preprocessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpanderInteractor"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class InstallDotNetTemplateInteractor<TExpander> : PreProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallDotNetTemplateInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public InstallDotNetTemplateInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute => Parameters.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpanderInteractor"/>.
        /// </summary>
        public override void Execute()
        {
            string templatePath = Path.Combine(Parameters.ExpandersFolder, Expander.Model.Name, Expander.Model.TemplateFolder);

            if (DirectoryService.Exists(templatePath))
            {
                string[] dotnetTemplateDirectories = DirectoryService.GetDirectories(templatePath, ".template.config", SearchOption.AllDirectories);
                foreach (string dotnetTemplateDirectory in dotnetTemplateDirectories)
                {
                    string path = DirectoryService.GetNameOfParentDirectory(dotnetTemplateDirectory);

                    Logger.Info($"Installing template from location {path}");
                    CommandLine.Start($"dotnet new install {path} --force");
                }
            }
        }
    }
}
