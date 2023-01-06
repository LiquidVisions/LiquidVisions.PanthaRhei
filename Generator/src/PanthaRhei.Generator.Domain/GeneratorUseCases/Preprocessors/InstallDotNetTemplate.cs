using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Preprocessors
{
    /// <summary>
    /// Install's the required dotnet visual studio templates that are required by the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander">A specific type of <see cref="IExpander"/>.</typeparam>
    public sealed class InstallDotNetTemplate<TExpander> : PreProcessor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallDotNetTemplate{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public InstallDotNetTemplate(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute => Parameters.Clean;

        /// <summary>
        /// Installs the dotnet templates that are part of the <see cref="IExpander"/>.
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
