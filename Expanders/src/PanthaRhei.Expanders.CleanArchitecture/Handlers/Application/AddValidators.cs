using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates validator classes.
    /// </summary>
    public class AddValidators : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddValidators"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddValidators(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 6;

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Application;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.ValidatorFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => "Validator";

        /// <inheritdoc/>
        protected override string TemplateName => Resources.ValidatorTemplate;

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity entity, string action)
        {
            return new
            {
                component,
                Action = action,
                Entity = entity,
            };
        }
    }
}
