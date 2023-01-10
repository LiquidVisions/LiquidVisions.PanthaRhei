using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    /// <summary>
    /// A base class representing a <seealso cref="AbstractHandlerInteractor{TExpander}"/> that is used when generating files for CRUD actions.
    /// </summary>
    public abstract class RequestActionsTemplateHandlerService : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestActionsTemplateHandlerService"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected RequestActionsTemplateHandlerService(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public virtual IProjectAgentInteractor ProjectAgent => projectAgent;

        /// <summary>
        /// Gets an array of the default Request Actions (Get, GetById, Create, Update, Delete).
        /// </summary>
        protected virtual string[] RequestActions => Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

        /// <summary>
        /// Gets the name of <seealso cref="Component"/> where the generated files are stored.
        /// </summary>
        protected abstract string RootLibraryName { get; }

        /// <summary>
        /// Gets the name of the folder where the generated files are stored.
        /// </summary>
        protected abstract string RootFolderName { get; }

        /// <summary>
        /// Gets the postfix of the filename of the generated file.
        /// </summary>
        protected abstract string FileNamePostFix { get; }

        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        protected abstract string TemplateName { get; }

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(RootLibraryName);
            string destinationFolder = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), RootFolderName);

            foreach (Entity endpoint in App.Entities)
            {
                string endpointFolder = System.IO.Path.Combine(destinationFolder, endpoint.Name.Pluralize());
                Directory.Create(endpointFolder);

                foreach (string action in RequestActions)
                {

                    string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, GetTemplateName(action));

                    object parameters = GetTemplateParameters(component, endpoint, action);

                    string result = templateService.Render(fullPathToTemplate, parameters);
                    File.WriteAllText(System.IO.Path.Combine(endpointFolder, $"{ToFileName(action, endpoint)}.cs"), result);
                }
            }
        }

        /// <summary>
        /// An override that gets the name of the template that needs to be adjusted with the action name.
        /// </summary>
        /// <param name="action">The action (Get, GetById, Update, Create, Delete).</param>
        /// <returns>The name of the template.</returns>
        protected virtual string GetTemplateName(string action)
        {
            return TemplateName;
        }

        /// <summary>
        /// Gets the template parameters that are used to generate the file.
        /// </summary>
        /// <param name="component"><seealso cref="Component"/></param>
        /// <param name="entity"><seealso cref="Entity"/></param>
        /// <param name="action">The action that is applicable for the template.</param>
        /// <returns>an object with parameters.</returns>
        protected abstract object GetTemplateParameters(Component component, Entity entity, string action);

        /// <summary>
        /// Gets the filename of the generated file file.
        /// </summary>
        /// <param name="action">The action that is applicable for the template.</param>
        /// <param name="entity"><seealso cref="Entity"/></param>
        /// <returns>The name of the file.</returns>
        protected virtual string ToFileName(string action, Entity entity) =>
            action switch
            {
                "Get" => $"Get{entity.Name.Pluralize()}{FileNamePostFix}",
                "GetById" => $"Get{entity.Name}ById{FileNamePostFix}",
                _ => $"{action}{entity.Name}{FileNamePostFix}"
            };
    }
}
