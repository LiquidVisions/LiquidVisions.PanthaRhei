using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates the DbContext class into the Infrastructure library.
    /// </summary>
    public class ExpandDatabaseContextHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly Parameters parameters;
        private readonly App app;
        private readonly Component domain;
        private readonly CleanArchitectureExpander expander;
        private readonly Component infrastructure;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandDatabaseContextHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandDatabaseContextHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            domain = Expander.Model.GetComponentByName(Resources.Domain);
            infrastructure = Expander.Model.GetComponentByName(Resources.EntityFramework);
        }

        public int Order => 9;

        public string Name => nameof(ExpandDatabaseContextHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            var templateModel = new
            {
                app.Entities,
                ConnectionString = app.ConnectionStrings.Single(x => x.Name == "DefaultConnectionString").Definition,
                NameSpace = infrastructure.GetComponentNamespace(app),
                NameSpaceEntities = domain.GetComponentNamespace(app, Resources.DomainEntityFolder),
            };

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.DbContextTemplate);
            string fullPathToComponent = projectAgent.GetComponentOutputFolder(infrastructure);
            string path = System.IO.Path.Combine(fullPathToComponent, "Context.cs");

            templateService.RenderAndSave(fullPathToTemplate, templateModel, path);
        }
    }
}