using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers
{
    /// <summary>
    /// Abstract class for dotnet cli commands.
    /// </summary>
    /// <typeparam name="TExpander">The <see cref="IExpander"/> that the handler belongs to.</typeparam>
    public abstract class AbstractScaffoldDotNetTemplateHandler<TExpander> : AbstractHandler<TExpander>
        where TExpander : class, IExpander
    {
        private readonly ICommandLine commandLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractScaffoldDotNetTemplateHandler{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><typeparamref name="TExpander"/>.</param>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected AbstractScaffoldDotNetTemplateHandler(TExpander expander, IDependencyResolver dependencyResolver)
            : base(expander, dependencyResolver)
        {
            commandLine = dependencyResolver.Get<ICommandLine>();
        }

        /// <summary>
        /// Gets the <see cref="ICommandLine"/>.
        /// </summary>
        protected ICommandLine CommandLine => commandLine;

        /// <summary>
        /// Creates a .csproj with the dotnet new command and and adds it to the solution.
        /// </summary>
        /// <param name="commandParameters">The dotnet cli command parameters.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="ns">The namespace that is used in the end result.</param>
        public virtual void Scaffold(string commandParameters, string name, string ns)
        {
            Logger.Info($"Creating directory {Parameters.OutputFolder}");
            CommandLine.Start($"mkdir {Parameters.OutputFolder}");

            Logger.Info($"Creating {name} @ {Parameters.OutputFolder}");
            CommandLine.Start($"dotnet new {commandParameters} --NAME {name} --ns {ns}", Parameters.OutputFolder);
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
