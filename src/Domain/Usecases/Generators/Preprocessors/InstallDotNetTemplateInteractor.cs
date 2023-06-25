using System.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    internal sealed class InstallDotNetTemplateInteractor<TExpander> : PreProcessorInteractor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallDotNetTemplateInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public InstallDotNetTemplateInteractor(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
        }

        /// <inheritdoc/>
        public override bool Enabled => Options.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpander"/>.
        /// </summary>
        public override void Execute()
        {
            string templatePath = Path.Combine(Options.ExpandersFolder, Expander.Model.Name, Expander.Model.TemplateFolder);

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
