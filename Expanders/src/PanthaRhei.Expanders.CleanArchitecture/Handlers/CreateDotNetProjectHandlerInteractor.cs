using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    /// <summary>
    /// Generates the solution and projects using the dotnet cli command liquidvisions-ca.
    /// </summary>
    public class CreateDotNetProjectHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IProjectTemplateInteractor projectTemplateInteractor;
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDotNetProjectHandlerInteractor "/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public CreateDotNetProjectHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            projectTemplateInteractor = dependencyFactory.Get<IProjectTemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            this.expander = expander;
        }

        /// <inheritdoc/>
        public bool Enabled => options.Clean;

        public virtual int Order => 1;

        public string Name => nameof(CreateDotNetProjectHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public virtual void Execute()
        {
            projectTemplateInteractor.CreateNew(Resources.TemplateShortName);

            Expander.Model.Components
                ?.ForEach(component => component.Packages
                ?.ForEach(package => projectTemplateInteractor.ApplyPackageOnComponent(component, package)));
        }
    }
}
