using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// An abstract implementation of the <see cref="IProcessorInteractor"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    public abstract class ProcessorInteractor<TExpander> : IProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly App app;
        private readonly ICommandLine commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;
        private readonly Parameters parameters;
        private readonly TExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected ProcessorInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            commandLine = dependencyFactory.Get<ICommandLine>();
            directoryService = dependencyFactory.Get<IDirectory>();
            logger = dependencyFactory.Get<ILogger>();
            parameters = dependencyFactory.Get<Parameters>();
            expander = dependencyFactory.Get<TExpander>();
        }

        /// <inheritdoc/>
        public App App => app;

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the <seealso cref="IDirectory"/>.
        /// </summary>
        public IDirectory DirectoryService => directoryService;

        /// <summary>
        /// Gets the <seealso cref="ICommandLine"/>.
        /// </summary>
        public ICommandLine CommandLine => commandLine;

        /// <summary>
        /// Gets the <seealso cref="Parameters"/>.
        /// </summary>
        public Parameters Parameters => parameters;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
