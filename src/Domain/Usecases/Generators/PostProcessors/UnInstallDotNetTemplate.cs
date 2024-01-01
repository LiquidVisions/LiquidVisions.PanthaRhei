using System.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UnInstallDotNetTemplate{TExpander}"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
    internal sealed class UnInstallDotNetTemplate<TExpander>(IDependencyFactory dependencyFactory) : PostProcessor<TExpander>(dependencyFactory)
        where TExpander : class, IExpander
    {
        /// <inheritdoc/>
        public override bool Enabled => Options.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpander"/>.
        /// </summary>
        public override void Execute()
        {
            string templatePath = Path.Combine(Options.ExpandersFolder, Expander.Model.Name, Resources.TemplatesFolder);

            string[] dotnetTemplateDirectories = DirectoryService.GetDirectories(templatePath, ".template.config", SearchOption.AllDirectories);
            foreach (string dotnetTemplateDirectory in dotnetTemplateDirectories)
            {
                string path = DirectoryService.GetNameOfParentDirectory(dotnetTemplateDirectory);

                Logger.Info($"Uninstalling template from location {path}");
                CommandLine.Start($"dotnet new uninstall {path}");
            }
        }
    }
}
