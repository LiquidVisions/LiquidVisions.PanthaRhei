using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Client
{
    /// <summary>
    /// a <seealso cref="RequestActionsTemplateHandlerService"/> that adds the request models to the output project.
    /// </summary>
    public class AddRequestModels : RequestActionsTemplateHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRequestModels"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddRequestModels(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
        }

        public override int Order => 18;

        /// <inheritdoc/>
        protected override string RootLibraryName => Resources.Client;

        /// <inheritdoc/>
        protected override string RootFolderName => Resources.RequestModelsFolder;

        /// <inheritdoc/>
        protected override string FileNamePostFix => throw new System.NotImplementedException();

        /// <inheritdoc/>
        protected override string TemplateName => Resources.RequestModelTemplate;

        /// <inheritdoc/>
        protected override string ToFileName(string action, Entity entity) =>
            action switch
            {
                "Get" => $"Get{entity.Name.Pluralize()}Query",
                "GetById" => $"Get{entity.Name}ByIdQuery",
                _ => $"{action}{entity.Name}Command"
            };

        /// <inheritdoc/>
        protected override object GetTemplateParameters(Component component, Entity entity, string action)
        {
            return new
            {
                Action = action,
                NS = Expander.Model.Name,
                NameSpace = $"{component.GetComponentNamespace(App, RootFolderName)}.{entity.Name.Pluralize()}",
                Entity = entity,
            };
        }
    }
}
