using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates the mappers for the application models.
    /// </summary>
    public class AddApplicationMappers : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddApplicationMappers"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddApplicationMappers(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 4;

        /// <inheritdoc/>
        protected override string[] RequestActions => new string[] { "Create", "Update" };

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Application;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.ApplicationMapperFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => "Mapper";

        /// <inheritdoc/>
        protected override string TemplateName => Resources.ApplicationMapperTemplate;

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity entity, string action)
        {
            return new
            {
                Action = action,
                NameSpace = component.GetComponentNamespace(App, RootFolderName),
                Entity = entity,
                Using = Expander.Model.Name,
            };
        }

        /// <inheritdoc/>
        protected override string ToFileName(string action, Entity entity)
        {
            return $"{action}{entity.Name}CommandTo{entity.Name}{FileNamePostFix}";
        }
    }
}
