using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    /// <summary>
    /// Generates the solution and projects using the dotnet cli command liquidvisions-ca.
    /// </summary>
    public class CreateDotNetProjectTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IApplication application;
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDotNetProjectTask "/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public CreateDotNetProjectTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            application = dependencyFactory.Get<IApplication>();
            options = dependencyFactory.Get<GenerationOptions>();
            this.expander = expander;
        }

        /// <inheritdoc/>
        public bool Enabled => options.Clean;

        /// <inheritdoc/>
        public virtual int Order => 1;

        /// <inheritdoc/>
        public string Name => nameof(CreateDotNetProjectTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public virtual void Execute()
        {
            application.MaterializeProject();

            //Expander.Model.Components
            //    ?.ForEach(component => component.Packages
            //    ?.ForEach(package => solution.ApplyPackageOnComponent(component, package)));
        }
    }
}
