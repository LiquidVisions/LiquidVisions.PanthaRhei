using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Add presenters classes to the output.
    /// </summary>
    public class AddPresenters : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPresenters"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddPresenters(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 14;

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Api;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.PresentersFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => "Presenter";

        /// <inheritdoc/>
        protected override string TemplateName => Resources.PresenterTemplate;

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity entity, string action)
        {
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application);
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);

            return new
            {
                clientComponent,
                applicationComponent,
                component,
                action,
                entity,
            };
        }
    }
}
