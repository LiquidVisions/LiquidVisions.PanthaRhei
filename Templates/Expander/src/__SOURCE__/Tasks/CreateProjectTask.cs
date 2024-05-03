using System;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace __PREFIX__.__SOURCE__
{
    /// <summary>
    /// An <seealso cref="IExpanderTask{T}"/> that creates the project."/>
    /// </summary>
    public class CreateProjectTask : IExpanderTask<__NAME__Expander>
    {
        private readonly __NAME__Expander expander;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IApplication application;
        private readonly GenerationOptions options;
        private readonly App app;

        public CreateProjectTask(__NAME__Expander expander, IDependencyFactory dependencyFactory)
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
        public string Name => nameof(CreateProjectTask);

        /// <inheritdoc/>
        public int Order => 1;

        /// <inheritdoc/>
        public __NAME__Expander Expander => expander;

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
