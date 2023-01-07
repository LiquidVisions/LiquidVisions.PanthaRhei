using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// a <seealso cref="RequestActionsTemplateHandlerService"/> that adds the boundaries to the output project.
    /// </summary>
    public class AddBoundaries : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddBoundaries"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddBoundaries(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 3;

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Application;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.ApplicationBoundariesFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => "Boundary";

        /// <inheritdoc/>
        protected override string TemplateName => Resources.BoundaryTemplate;

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity endpoint, string action)
        {
            return new
            {
                ActionType = action,
                NameSpace = component.GetComponentNamespace(App, RootFolderName),
                Using = Expander.Model.Name,
                Entity = endpoint,
            };
        }
    }
}
