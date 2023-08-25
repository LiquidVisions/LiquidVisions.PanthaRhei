using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain.Tasks
{
    public class CreateProjectSolutionTask : IExpanderTask<DomainExpander>
    {
        private readonly DomainExpander expander;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IApplication application;
        private readonly GenerationOptions options;
        private readonly App app;

        public CreateProjectSolutionTask(DomainExpander expander, IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            application = dependencyFactory.Get<IApplication>();
            app = dependencyFactory.Get<App>();

            this.expander = expander;
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public string Name => nameof(CreateProjectSolutionTask);

        /// <inheritdoc/>
        public int Order => 1;

        /// <inheritdoc/>
        public DomainExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.Clean;

        /// <inheritdoc/>
        public void Execute()
        {
            Component component = expander.Model.Components.Single();

            application.MaterializeComponent(component);
        }
    }
}
