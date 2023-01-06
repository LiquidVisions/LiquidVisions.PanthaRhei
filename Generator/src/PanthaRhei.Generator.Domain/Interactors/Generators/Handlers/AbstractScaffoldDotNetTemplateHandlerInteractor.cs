using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers
{
    /// <summary>
    /// Abstract class for dotnet cli commands.
    /// </summary>
    /// <typeparam name="TExpander">The <see cref="IExpanderInteractor"/> that the handler belongs to.</typeparam>
    public abstract class AbstractScaffoldDotNetTemplateHandlerInteractor<TExpander> : AbstractHandlerInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly ICommandLineInteractor commandLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractScaffoldDotNetTemplateHandlerInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><typeparamref name="TExpander"/>.</param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected AbstractScaffoldDotNetTemplateHandlerInteractor(TExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
        }

        /// <summary>
        /// Gets the <see cref="ICommandLineInteractor"/>.
        /// </summary>
        protected ICommandLineInteractor CommandLine => commandLine;

        /// <summary>
        /// Creates a .csproj with the dotnet new command and and adds it to the solution.
        /// </summary>
        /// <param name="commandParameters">The dotnet cli command parameters.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="ns">The namespace that is used in the end result.</param>
        public virtual void Scaffold(string commandParameters, string name, string ns)
        {
            string outputFolder = Path.Combine(Parameters.OutputFolder, ns);

            Logger.Info($"Creating directory {outputFolder}");
            CommandLine.Start($"mkdir {outputFolder}");

            Logger.Info($"Creating {name} @ {outputFolder}");
            CommandLine.Start($"dotnet new {commandParameters} --NAME {name} --ns {ns}", outputFolder);
        }

        /// <summary>
        /// Ads a project reference to the source project.
        /// </summary>
        /// <param name="pathToTargetLibrary">The source project.</param>
        /// <param name="pathToCurrentLibrary">The project reference that will be added.</param>
        public virtual void ApplyProjectReferences(string pathToTargetLibrary, string pathToCurrentLibrary)
        {
            Logger.Info($"Create library reference to {pathToTargetLibrary} with reference to {pathToCurrentLibrary}");

            CommandLine.Start($"dotnet add \"{pathToCurrentLibrary}\" reference \"{pathToTargetLibrary}\"");
        }

        /// <summary>
        /// Creates a package reference on an existing library.
        /// </summary>
        /// <param name="project">The source project.</param>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="version">The version of the package.</param>
        public virtual void ApplyNugetPackages(string project, string packageName, string version)
        {
            Logger.Info($"Adding nuget package {packageName} to {project}");

            CommandLine.Start($"dotnet add \"{project}\" package \"{packageName}\" --version {version} -n");
        }
    }
}
