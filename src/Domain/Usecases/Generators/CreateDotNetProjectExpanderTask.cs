using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using System;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators
{
    /// <summary>
    /// Represents a task that creates a .NET project.
    /// </summary>
    /// <typeparam name="TExpander">The Expander</typeparam>
    public class CreateDotNetProjectExpanderTask<TExpander> : IExpanderTask<TExpander>
        where TExpander : class, IExpander
    {
        private readonly TExpander expander;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IApplication application;
        private readonly GenerationOptions options;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDotNetProjectExpanderTask{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"></param>
        /// <param name="dependencyFactory"></param>
        public CreateDotNetProjectExpanderTask(TExpander expander, IDependencyFactory dependencyFactory)
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
        public int Order => 0;

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.Clean;

        /// <inheritdoc/>
        public void Execute()
        {
            Component component = expander.Model
                .Components
                .Single();

            application.MaterializeComponent(component);
        }
    }
}
