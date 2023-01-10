using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// a <seealso cref="RequestActionsTemplateHandlerService"/> that adds the request models to the output project.
    /// </summary>
    public class AddInteractors : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddInteractors"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddInteractors(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 5;

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Application;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.InteractorFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => "Interactor";

        /// <inheritdoc/>
        protected override string TemplateName => Resources.InteractorTemplate;

        /// <inheritdoc/>
        protected override string GetTemplateName(string action)
        {
            return $"{action}{TemplateName}";
        }

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity entity, string action)
        {
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);

            return new
            {
                clientComponent,
                component,
                Entity = entity,
            };
        }
    }
}
