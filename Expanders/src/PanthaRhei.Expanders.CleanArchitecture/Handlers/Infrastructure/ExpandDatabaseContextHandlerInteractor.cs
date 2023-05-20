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
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
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

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            domain = Expander.GetComponentByName(Resources.Domain);
            infrastructure = Expander.GetComponentByName(Resources.EntityFramework);
        }

        public int Order => 9;

        public string Name => nameof(ExpandDatabaseContextHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            var templateModel = new
            {
                app.Entities,
                ConnectionString = app.ConnectionStrings.Single().Definition,
                NameSpace = infrastructure.GetComponentNamespace(app),
                NameSpaceEntities = domain.GetComponentNamespace(app, Resources.DomainEntityFolder),
            };

            string fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.DbContextTemplate);
            string fullPathToComponent = expander.GetComponentOutputFolder(infrastructure);
            string path = System.IO.Path.Combine(fullPathToComponent, "Context.cs");

            templateService.RenderAndSave(fullPathToTemplate, templateModel, path);
        }
    }
}