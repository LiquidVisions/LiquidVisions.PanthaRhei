using System;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain.Tasks
{
    /// <summary>
    /// An <seealso cref="IExpanderTask{T}"/> that creates the project."/>
    /// </summary>
    public class CreateProjectSolutionTask : IExpanderTask<DomainExpander>
    {
        private readonly DomainExpander expander;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IApplication application;
        private readonly GenerationOptions options;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectSolutionTask"/> class.
        /// </summary>
        /// <param name="expander"></param>
        /// <param name="dependencyFactory"></param>
        public CreateProjectSolutionTask(DomainExpander expander, IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(expander, nameof(expander));
            ArgumentNullException.ThrowIfNull(dependencyFactory, nameof(dependencyFactory));

            options = dependencyFactory.Resolve<GenerationOptions>();
            application = dependencyFactory.Resolve<IApplication>();
            app = dependencyFactory.Resolve<App>();

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
